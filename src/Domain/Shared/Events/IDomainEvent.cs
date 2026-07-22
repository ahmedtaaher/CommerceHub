namespace Domain.Shared.Events
{
  public interface IDomainEvent
  {
    DateTime OccurredOn { get;}
  }
}