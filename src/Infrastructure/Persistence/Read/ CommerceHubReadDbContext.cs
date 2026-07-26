using Domain.Catalog.Entities;
using Infrastructure.Persistence.Read.Configurations;
using Infrastructure.Persistence.Read.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Read
{
  public sealed class CommerceHubReadDbContext : DbContext
  {
    public CommerceHubReadDbContext(DbContextOptions<CommerceHubReadDbContext> options) : base(options)
    {
      
    }

    public DbSet<ProductReadModel> Products => Set<ProductReadModel>();
    public DbSet<CategoryReadModel> Categories => Set<CategoryReadModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ProductReadConfiguration());
      modelBuilder.ApplyConfiguration(new CategoryReadConfiguration());

      base.OnModelCreating(modelBuilder);
    }
  }
}