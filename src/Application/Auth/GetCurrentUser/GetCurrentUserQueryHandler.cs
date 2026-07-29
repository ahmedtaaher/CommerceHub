using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Shared.Errors;

namespace Application.Auth.GetCurrentUser
{
  public sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, GetCurrentUserResponse>
  {
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;

    public GetCurrentUserQueryHandler(ICurrentUser currentUser, IUserService userService)
    {
      _currentUser = currentUser;
      _userService = userService;
    }

    public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
      var user = await _userService.GetProfileAsync(_currentUser.UserId, cancellationToken);

      if (user is null)
      {
        return Result<GetCurrentUserResponse>.Failure(new Error("Auth.UserNotFound", "User not found."));
      }

      return Result<GetCurrentUserResponse>.Success(new GetCurrentUserResponse(
        user.Value.Id,
        user.Value.FirstName,
        user.Value.LastName,
        user.Value.Email,
        user.Value.Roles.ToList()));
    }
  }
}