using Application.Abstractions.Messaging;

namespace Application.Auth.ChangePassword
{
  public sealed record ChangePasswordCommand(
    Guid UserId,
    string CurrentPassword,
    string NewPassword
  ) : ICommand;
}