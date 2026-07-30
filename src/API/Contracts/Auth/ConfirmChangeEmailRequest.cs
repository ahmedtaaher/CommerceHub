namespace API.Contracts.Auth
{
  public sealed record ConfirmChangeEmailRequest(Guid UserId, string NewEmail, string Token);
}