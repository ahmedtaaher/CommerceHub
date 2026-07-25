using Application.Abstractions.Persistence;
using Domain.Catalog.Entities;
using Domain.Shared.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write
{
  public sealed class CommerceHubWriteDbContext : DbContext, IUnitOfWork
  {
    private readonly IPublisher _publisher;

    public CommerceHubWriteDbContext(DbContextOptions<CommerceHubWriteDbContext> options, IPublisher publisher) : base(options)
    {
      _publisher = publisher;
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommerceHubWriteDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var domainEvents = ChangeTracker.Entries<AggregateRoot<Guid>>().SelectMany(x => x.Entity.DomainEvents).ToList();

      var result = await base.SaveChangesAsync(cancellationToken);

      foreach (var domainEvent in domainEvents)
      {
        await _publisher.Publish(domainEvent, cancellationToken);
      }

      foreach (var aggregate in ChangeTracker.Entries<AggregateRoot<Guid>>())
      {
        aggregate.Entity.ClearDomainEvents();
      }

      return result;
    }
  }
}