namespace Application.Common.Authentication
{
  public sealed record TokenResponse(
    string AccessToken,
    DateTime ExpiresAt
  );
}