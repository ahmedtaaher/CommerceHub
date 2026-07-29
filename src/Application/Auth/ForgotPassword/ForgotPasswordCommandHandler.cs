using Application.Abstractions.Email;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.ForgotPassword
{
  public sealed class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
  {
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(IUserService userService, IEmailService emailService)
    {
      _userService = userService;
      _emailService = emailService;
    }

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
      var token = await _userService.GeneratePasswordResetTokenAsync(request.Email, cancellationToken);

      if (token is null)
        return Result.Success();

      await _emailService.SendAsync(request.Email, "Reset your password", $"Your password reset token is: {token}", cancellationToken);

      return Result.Success();
    }
  }
}