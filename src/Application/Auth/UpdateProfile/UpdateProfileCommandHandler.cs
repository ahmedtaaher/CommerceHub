using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.UpdateProfile
{
  public sealed class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand>
  {
    private readonly IUserService _userService;
    private readonly ICurrentUser _currentUser;

    public UpdateProfileCommandHandler(IUserService userService, ICurrentUser currentUser)
    {
      _userService = userService;
      _currentUser = currentUser;
    }

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
      return await _userService.UpdateProfileAsync(_currentUser.UserId, request.FirstName, request.LastName, cancellationToken);
    }
  }
}