
namespace Domain.Shared.Events
{
  public abstract record DomainEvent : IDomainEvent
  {
    public DateTime OccurredOn { get ;} = DateTime.UtcNow;
  }
}