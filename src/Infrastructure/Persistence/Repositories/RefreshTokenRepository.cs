using Application.Abstractions.Persistence;
using Domain.Identity.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
  public sealed class RefreshTokenRepository : IRefreshTokenRepository
  {
    private readonly CommerceHubIdentityDbContext _context;

    public RefreshTokenRepository(CommerceHubIdentityDbContext context)
    {
      _context = context;
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
      await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public async Task<RefreshToken?> GetByHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
      return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);
    }

    public Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
      _context.RefreshTokens.Update(refreshToken);

      return Task.CompletedTask;
    }

    public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
      return await _context.RefreshTokens.Where(x => x.UserId == userId && x.RevokedAt == null && x.ExpiresAt > DateTime.UtcNow).ToListAsync(cancellationToken);
    }
  }
}