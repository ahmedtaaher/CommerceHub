
namespace Domain.Shared.Events
{
  public abstract class DomainEvent : IDomainEvent
  {
    public DateTime OccurredOn { get ;} = DateTime.UtcNow;
  }
}