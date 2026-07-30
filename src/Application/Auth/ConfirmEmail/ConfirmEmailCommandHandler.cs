using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.ConfirmEmail
{
  public sealed class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
  {
    private readonly IUserService _userService;

    public ConfirmEmailCommandHandler(IUserService userService)
    {
      _userService = userService;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
      return await _userService.ConfirmEmailAsync(request.Email, request.Token, cancellationToken);
    }
  }
}