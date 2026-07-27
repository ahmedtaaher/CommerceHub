using Application.Abstractions.Messaging;
using Application.Common.Authentication;

namespace Application.Auth.Login
{
  public sealed record LoginCommand(
    string Email,
    string Password
  ) : ICommand<TokenResponse>;

}