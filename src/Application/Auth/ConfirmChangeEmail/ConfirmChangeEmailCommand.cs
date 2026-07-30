using Application.Abstractions.Messaging;

namespace Application.Auth.ConfirmChangeEmail
{
  public sealed record ConfirmChangeEmailCommand(
    Guid UserId,
    string NewEmail,
    string Token
  ) : ICommand;

}