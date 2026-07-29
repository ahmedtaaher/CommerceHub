namespace Application.Common.Authentication
{
  public sealed record TokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
  );
}