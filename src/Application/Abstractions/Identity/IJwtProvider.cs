using Application.Common.Authentication;

namespace Application.Abstractions.Identity
{
  public interface IJwtProvider
  {
    TokenResponse GenerateAsync(Guid userId, string email, IEnumerable<string> roles);
  }
}