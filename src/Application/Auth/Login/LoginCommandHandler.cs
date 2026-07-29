using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Application.Common.Authentication;
using Domain.Identity.Entities;
using Domain.Shared.Errors;

namespace Application.Auth.Login
{
  public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, TokenResponse>
  {
    private readonly IUserService _userService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenHasher _tokenHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;

    public LoginCommandHandler(IUserService userService, IJwtProvider jwtProvider, IRefreshTokenGenerator refreshTokenGenerator, ITokenHasher tokenHasher, IRefreshTokenRepository refreshTokenRepository, IIdentityUnitOfWork identityUnitOfWork)
    {
      _userService = userService;
      _jwtProvider = jwtProvider;
      _refreshTokenGenerator = refreshTokenGenerator;
      _tokenHasher = tokenHasher;
      _refreshTokenRepository = refreshTokenRepository;
      _identityUnitOfWork = identityUnitOfWork;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
      var user = await _userService.LoginAsync(request.Email, request.Password, cancellationToken);

      if (user is null)
      {
        return Result<TokenResponse>.Failure(new Error("Auth.InvalidCredentials", "Invalid email or password."));
      }

      var (accessToken, expiresAt) = _jwtProvider.GenerateAsync(user.Value.Id, user.Value.Email, user.Value.Roles);

      var refreshToken = _refreshTokenGenerator.Generate();

      var refreshTokenHash = _tokenHasher.Hash(refreshToken);

      var refreshTokenResult = RefreshToken.Create(user.Value.Id, refreshTokenHash, DateTime.UtcNow.AddDays(7));

      if (refreshTokenResult.IsFailure)
      {
        return Result<TokenResponse>.Failure(refreshTokenResult.Error);
      }

      await _refreshTokenRepository.AddAsync(refreshTokenResult.Value, cancellationToken);

      await _identityUnitOfWork.SaveChangesAsync(cancellationToken);

      return Result<TokenResponse>.Success(new TokenResponse(accessToken, refreshToken, expiresAt));
    }
  }
}