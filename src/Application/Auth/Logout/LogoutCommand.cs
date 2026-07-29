using Application.Abstractions.Messaging;

namespace Application.Auth.Logout
{
  public sealed record LogoutCommand(string RefreshToken) : ICommand;

}