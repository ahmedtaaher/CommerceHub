using Application.Abstractions.Messaging;

namespace Application.Auth.ResendConfirmationEmail
{
  public sealed record ResendConfirmationEmailCommand(string Email) : ICommand;

}