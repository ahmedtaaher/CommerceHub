namespace Domain.Shared.Abstractions
{
  public abstract class BaseEntity<TId>
  {
    protected BaseEntity(TId id)
    {
      Id = id;
    }
    public TId Id { get; protected set; }
  }
}