using Application.Abstractions.Identity;
using Application.Abstractions.Persistence;
using Domain.Catalog.Entities;
using Domain.Shared.Abstractions;
using Infrastructure.Persistence.Write.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write
{
  public sealed class CommerceHubWriteDbContext : DbContext, IUnitOfWork
  {
    private readonly IPublisher _publisher;
    private readonly IUserContext _userContext;

    public CommerceHubWriteDbContext(DbContextOptions<CommerceHubWriteDbContext> options, IPublisher publisher, IUserContext userContext) : base(options)
    {
      _publisher = publisher;
      _userContext = userContext;
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ProductConfiguration());
      modelBuilder.ApplyConfiguration(new CategoryConfiguration());

      base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      ApplyAuditInformation();
      
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

    private void ApplyAuditInformation()
    {
      var userId = _userContext.UserId;
      var now = DateTime.UtcNow;

      foreach (var entry in ChangeTracker.Entries<AuditableEntity<Guid>>())
      {
        if (entry.State == EntityState.Added)
        {
          entry.Entity.SetCreatedAudit(userId, now);
          continue;
        }

        if (entry.State != EntityState.Modified)
          continue;

        var isSoftDelete = !entry.Property(x => x.IsDeleted).OriginalValue && entry.Property(x => x.IsDeleted).CurrentValue;

        if (isSoftDelete)
        {
          entry.Entity.SetDeletedAudit(userId, now);
          continue;
        }

        entry.Entity.SetModifiedAudit(userId, now);
      }
    }
  }
}