using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Identity.Errors;
using Domain.Shared.Errors;

namespace Application.Auth.Logout
{
  public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
  {
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHasher _tokenHasher;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;

    public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository, ITokenHasher tokenHasher, IIdentityUnitOfWork identityUnitOfWork)
    {
      _refreshTokenRepository = refreshTokenRepository;
      _tokenHasher = tokenHasher;
      _identityUnitOfWork = identityUnitOfWork;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
      var tokenHash = _tokenHasher.Hash(request.RefreshToken);

      var refreshToken = await _refreshTokenRepository.GetByHashAsync(tokenHash, cancellationToken);

      if (refreshToken is null)
      {
        return Result.Failure(IdentityErrors.InvalidRefreshToken);
      }

      refreshToken.Revoke(reason: "User logged out");

      await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

      await _identityUnitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
    }
  }
}