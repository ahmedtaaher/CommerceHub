using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Shared.Errors;

namespace Application.Auth.ResetPassword
{
  public sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
  {
    private readonly IUserService _userService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;

    public ResetPasswordCommandHandler(IUserService userService, IRefreshTokenRepository refreshTokenRepository, IIdentityUnitOfWork identityUnitOfWork)
    {
      _userService = userService;
      _refreshTokenRepository = refreshTokenRepository;
      _identityUnitOfWork = identityUnitOfWork;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
      var result = await _userService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword, cancellationToken);

      if (result.IsFailure)
      {
        return result;
      }

      var user = await _userService.LoginAsync(request.Email, request.NewPassword, cancellationToken);

      if (user is null)
      {
        return Result.Success();
      }

      var tokens = await _refreshTokenRepository.GetActiveTokensByUserIdAsync(user.Value.Id, cancellationToken);

      foreach (var token in tokens)
      {
        token.Revoke(reason: "Password reset");
      }

      await _identityUnitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
    }
  }
}