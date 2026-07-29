using Application.Abstractions.Messaging;
using Application.Common.Authentication;

namespace Application.Auth.Refresh
{
  public sealed record RefreshTokenCommand(
    string RefreshToken
  ) : ICommand<TokenResponse>;
}