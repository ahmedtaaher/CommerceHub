using Domain.Identity.Errors;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Identity.Entities
{
  public sealed class RefreshToken : BaseEntity<Guid>
  {
    private RefreshToken()
    {
      
    }

    private RefreshToken(Guid id, Guid userId, string tokenHash, DateTime expiresAt, DateTime createdAt) : base(id)
    {
      UserId = userId;
      TokenHash = tokenHash;
      ExpiresAt = expiresAt;
      CreatedAt = createdAt;
    }

    public Guid UserId { get; private set; }

    public string TokenHash { get; private set; } = string.Empty;

    public DateTime ExpiresAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    public string? ReplacedByTokenHash { get; private set; }

    public string? RevokedReason { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsRevoked => RevokedAt.HasValue;

    public bool IsActive => !IsExpired && !IsRevoked;

    public static Result<RefreshToken> Create(Guid userId, string tokenHash, DateTime expiresAt)
    {
      if (userId == Guid.Empty)
        return Result<RefreshToken>.Failure(new Error("Identity.InvalidUser", "UserId is required."));

      if (string.IsNullOrWhiteSpace(tokenHash))
        return Result<RefreshToken>.Failure(IdentityErrors.InvalidRefreshToken);

      var token = new RefreshToken(Guid.NewGuid(), userId, tokenHash, expiresAt, DateTime.UtcNow);

      return Result<RefreshToken>.Success(token);
    }

    public Result Revoke(string? replacedByTokenHash = null, string? reason = null)
    {
      if (IsRevoked)
        return Result.Success();

      RevokedAt = DateTime.UtcNow;
      ReplacedByTokenHash = replacedByTokenHash;
      RevokedReason = reason;

      return Result.Success();
    }
  }
}