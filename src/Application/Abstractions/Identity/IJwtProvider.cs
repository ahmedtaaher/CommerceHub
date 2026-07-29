using Application.Common.Authentication;

namespace Application.Abstractions.Identity
{
  public interface IJwtProvider
  {
    (string AccessToken, DateTime ExpiresAt) GenerateAsync(Guid userId, string email, IEnumerable<string> roles);
  }
}