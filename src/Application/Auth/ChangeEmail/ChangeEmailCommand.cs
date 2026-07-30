using Application.Abstractions.Messaging;

namespace Application.Auth.ChangeEmail
{
  public sealed record ChangeEmailCommand(
    Guid UserId,
    string NewEmail
  ) : ICommand;

}