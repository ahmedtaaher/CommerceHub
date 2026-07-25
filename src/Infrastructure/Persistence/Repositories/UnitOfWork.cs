using Application.Abstractions.Persistence;
using Infrastructure.Persistence.Write;

namespace Infrastructure.Persistence.Repositories
{
  public sealed class UnitOfWork : IUnitOfWork
  {
    private readonly CommerceHubWriteDbContext _context;

    public UnitOfWork(CommerceHubWriteDbContext context)
    {
      _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return await _context.SaveChangesAsync(cancellationToken);
    }
  }
}