using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Application.Common.Authentication;
using Domain.Identity.Entities;
using Domain.Identity.Errors;
using Domain.Shared.Errors;

namespace Application.Auth.Refresh
{
  public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, TokenResponse>
  {
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenHasher _tokenHasher;
    private readonly IUserService _userService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;

    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IRefreshTokenGenerator refreshTokenGenerator, ITokenHasher tokenHasher, IUserService userService, IJwtProvider jwtProvider, IIdentityUnitOfWork identityUnitOfWork)
    {
      _refreshTokenRepository = refreshTokenRepository;
      _refreshTokenGenerator = refreshTokenGenerator;
      _tokenHasher = tokenHasher;
      _userService = userService;
      _jwtProvider = jwtProvider;
      _identityUnitOfWork = identityUnitOfWork;
    }
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
      var tokenHash = _tokenHasher.Hash(request.RefreshToken);

      var storedToken = await _refreshTokenRepository.GetByHashAsync(tokenHash, cancellationToken);

      if (storedToken is null)
      {
        return Result<TokenResponse>.Failure(IdentityErrors.InvalidRefreshToken);
      }

      if (!storedToken.IsActive)
      {
        return Result<TokenResponse>.Failure(IdentityErrors.InvalidRefreshToken);
      }

      var user = await _userService.GetByIdAsync(storedToken.UserId, cancellationToken);

      if (user is null)
      {
        return Result<TokenResponse>.Failure(new Error("Auth.UserNotFound", "User not found."));
      }

      var (accessToken, expiresAt) = _jwtProvider.GenerateAsync(user.Value.Id, user.Value.Email, user.Value.Roles);

      var refreshToken = _refreshTokenGenerator.Generate();

      var refreshTokenHash = _tokenHasher.Hash(refreshToken);

      storedToken.Revoke(refreshTokenHash, "Refresh token rotated");

      var newRefreshTokenResult = RefreshToken.Create(user.Value.Id, refreshTokenHash, DateTime.UtcNow.AddDays(7));

      if (newRefreshTokenResult.IsFailure)
      {
        return Result<TokenResponse>.Failure(newRefreshTokenResult.Error);
      }

      await _refreshTokenRepository.AddAsync(newRefreshTokenResult.Value, cancellationToken);

      await _refreshTokenRepository.UpdateAsync(storedToken, cancellationToken);

      await _identityUnitOfWork.SaveChangesAsync(cancellationToken);

      return Result<TokenResponse>.Success(new TokenResponse(accessToken, refreshToken, expiresAt));
    }
  }
}