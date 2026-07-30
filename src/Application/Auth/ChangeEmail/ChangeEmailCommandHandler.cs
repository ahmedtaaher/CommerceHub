using Application.Abstractions.Email;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.ChangeEmail
{
  public sealed class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand>
  {
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public ChangeEmailCommandHandler(IUserService userService, IEmailService emailService)
    {
      _userService = userService;
      _emailService = emailService;
    }

    public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
      var token = await _userService.GenerateChangeEmailTokenAsync(request.UserId, request.NewEmail, cancellationToken);

      if (token is null)
      {
        return Result.Failure(new Error("Auth.UserNotFound", "User not found."));
      }

      await _emailService.SendAsync(request.NewEmail, "Confirm your new email", $"Confirmation token:\n\n{token}", cancellationToken);

      return Result.Success();
    }
  }
}