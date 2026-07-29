namespace Application.Abstractions.Identity
{
  public interface IIdentityUnitOfWork
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }
}