using Application.Abstractions.Email;
using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Common.Authorization;
using Domain.Shared.Errors;

namespace Application.Auth.Register
{
  public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
  {
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(IUserService userService, IEmailService emailService)
    {
      _userService = userService;
      _emailService = emailService;
    }
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
      var exists = await _userService.ExistsByEmailAsync(request.Email, cancellationToken);

      if (exists)
      {
        return Result<Guid>.Failure(new Error("Auth.EmailExists", "Email already exists."));
      }

      var userId = await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password, cancellationToken);

      var confirmationToken = await _userService.GenerateEmailConfirmationTokenAsync(userId, cancellationToken);

      if (confirmationToken is not null)
      {
        var body = $""" Welcome to CommerceHub! Please confirm your email using the following token: {confirmationToken} """;

        await _emailService.SendAsync(request.Email, "Confirm your email", body, cancellationToken);
      }

      return Result<Guid>.Success(userId);
    }
  }
}