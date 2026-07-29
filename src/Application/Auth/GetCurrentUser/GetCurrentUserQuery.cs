using Application.Abstractions.Messaging;

namespace Application.Auth.GetCurrentUser
{
  public sealed record GetCurrentUserQuery : IQuery<GetCurrentUserResponse>;

}