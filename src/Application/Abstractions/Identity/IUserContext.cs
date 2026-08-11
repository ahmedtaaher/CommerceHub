namespace Application.Abstractions.Identity
{
  public interface IUserContext
  {
    Guid? UserId { get; }

    bool IsAuthenticated { get; }
  }
}