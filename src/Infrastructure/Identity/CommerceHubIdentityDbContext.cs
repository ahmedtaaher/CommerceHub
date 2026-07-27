using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
  public sealed class CommerceHubIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
  {
    public CommerceHubIdentityDbContext(DbContextOptions<CommerceHubIdentityDbContext> options) : base(options)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }
  }
}