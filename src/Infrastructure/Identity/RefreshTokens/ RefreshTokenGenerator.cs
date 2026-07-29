using System.Security.Cryptography;
using Application.Abstractions.Identity;

namespace Infrastructure.Identity.RefreshTokens
{
  public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
  {
    public string Generate()
    {
      var bytes = RandomNumberGenerator.GetBytes(64);

      return Convert.ToBase64String(bytes);
    }
  }
}