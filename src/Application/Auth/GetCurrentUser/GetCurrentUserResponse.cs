namespace Application.Auth.GetCurrentUser
{
  public sealed record GetCurrentUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IReadOnlyCollection<string> Roles
  );

}