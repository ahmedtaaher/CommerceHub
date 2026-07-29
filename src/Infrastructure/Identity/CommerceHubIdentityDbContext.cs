using Domain.Identity.Entities;
using Infrastructure.Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
  public sealed class CommerceHubIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
  {
    public CommerceHubIdentityDbContext(DbContextOptions<CommerceHubIdentityDbContext> options) : base(options)
    {
      
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfiguration(new RefreshTokenConfiguration());
    }
  }
}