using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Common.Authentication;
using Domain.Shared.Errors;

namespace Application.Auth.Login
{
  public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, TokenResponse>
  {
    private readonly IUserService _userService;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUserService userService, IJwtProvider jwtProvider)
    {
      _userService = userService;
      _jwtProvider = jwtProvider;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
      var user = await _userService.LoginAsync(request.Email, request.Password, cancellationToken);

      if (user is null)
      {
        return Result<TokenResponse>.Failure(new Error("Auth.InvalidCredentials", "Invalid email or password."));
      }

      var token = _jwtProvider.GenerateAsync(user.Value.Id, user.Value.Email, user.Value.Roles);

      return Result<TokenResponse>.Success(token);
    }
  }
}