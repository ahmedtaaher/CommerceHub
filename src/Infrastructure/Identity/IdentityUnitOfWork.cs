using Application.Abstractions.Identity;

namespace Infrastructure.Identity
{
  public sealed class IdentityUnitOfWork : IIdentityUnitOfWork
  {
    private readonly CommerceHubIdentityDbContext _context;

    public IdentityUnitOfWork(CommerceHubIdentityDbContext context)
    {
      _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return _context.SaveChangesAsync(cancellationToken);
    }
  }
}