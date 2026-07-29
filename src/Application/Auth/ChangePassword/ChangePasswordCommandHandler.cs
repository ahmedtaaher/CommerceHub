using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Shared.Errors;

namespace Application.Auth.ChangePassword
{
  public sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
  {
    private readonly IUserService _userService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;

    public ChangePasswordCommandHandler(IUserService userService, IRefreshTokenRepository refreshTokenRepository, IIdentityUnitOfWork identityUnitOfWork)
    {
      _userService = userService;
      _refreshTokenRepository = refreshTokenRepository;
      _identityUnitOfWork = identityUnitOfWork;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
      var result = await _userService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword, cancellationToken);

      if (result.IsFailure)
      {
        return result;
      }

      var refreshTokens = await _refreshTokenRepository.GetActiveTokensByUserIdAsync(request.UserId, cancellationToken);

      foreach (var refreshToken in refreshTokens)
      {
        refreshToken.Revoke(reason: "Password changed");
      }

      await _identityUnitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
    }
  }
}