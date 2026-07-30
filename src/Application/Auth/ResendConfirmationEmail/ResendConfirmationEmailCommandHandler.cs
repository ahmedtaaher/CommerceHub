using Application.Abstractions.Email;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.ResendConfirmationEmail
{
  public sealed class ResendConfirmationEmailCommandHandler : ICommandHandler<ResendConfirmationEmailCommand>
  {
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public ResendConfirmationEmailCommandHandler(IUserService userService, IEmailService emailService)
    {
      _userService = userService;
      _emailService = emailService;
    }

    public async Task<Result> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
      var token = await _userService.GenerateEmailConfirmationTokenByEmailAsync(request.Email, cancellationToken);

      if (token is null)
        return Result.Success();

      await _emailService.SendAsync(request.Email, "Confirm your email", $"Confirmation token:\n\n{token}", cancellationToken);

      return Result.Success();
    }
  }
}