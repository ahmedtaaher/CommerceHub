using Application.Abstractions.Messaging;

namespace Application.Auth.Register
{
  public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
  ) : ICommand<Guid>;
}