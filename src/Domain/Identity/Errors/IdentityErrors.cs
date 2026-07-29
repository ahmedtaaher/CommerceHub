using Domain.Shared.Errors;

namespace Domain.Identity.Errors
{
  public static class IdentityErrors
  {
    public static readonly Error RefreshTokenExpired = new("Identity.RefreshTokenExpired", "Refresh token has expired.");

    public static readonly Error RefreshTokenRevoked = new("Identity.RefreshTokenRevoked", "Refresh token has been revoked.");

    public static readonly Error InvalidRefreshToken = new("Identity.InvalidRefreshToken", "Refresh token is invalid.");
  }
}