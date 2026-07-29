using Application.Abstractions.Messaging;

namespace Application.Auth.UpdateProfile
{
  public sealed record UpdateProfileCommand(
    string FirstName,
    string LastName
  ) : ICommand;
}