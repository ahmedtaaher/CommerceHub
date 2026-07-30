using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.ConfirmChangeEmail
{
  public sealed class ConfirmChangeEmailCommandHandler : ICommandHandler<ConfirmChangeEmailCommand>
  {
    private readonly IUserService _userService;

    public ConfirmChangeEmailCommandHandler(IUserService userService)
    {
      _userService = userService;
    }

    public async Task<Result> Handle(ConfirmChangeEmailCommand request, CancellationToken cancellationToken)
    {
      return await _userService.ChangeEmailAsync(request.UserId, request.NewEmail, request.Token, cancellationToken);
    }
  }
}