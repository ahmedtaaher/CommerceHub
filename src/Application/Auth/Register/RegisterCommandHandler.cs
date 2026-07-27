using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Application.Common.Authorization;
using Domain.Shared.Errors;

namespace Application.Auth.Register
{
  public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
  {
    private readonly IUserService _userService;

    public RegisterCommandHandler(IUserService userService)
    {
      _userService = userService;
    }
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
      var exists = await _userService.ExistsByEmailAsync(request.Email, cancellationToken);

      if (exists)
      {
        return Result<Guid>.Failure(new Error("Auth.EmailExists", "Email already exists."));
      }

      var id = await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password, cancellationToken);

      await _userService.AddToRoleAsync(id, Roles.Viewer, cancellationToken);

      return Result<Guid>.Success(id);
    }
  }
}