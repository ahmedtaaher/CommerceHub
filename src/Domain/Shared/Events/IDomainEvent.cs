using MediatR;

namespace Domain.Shared.Events
{
  public interface IDomainEvent : INotification
  {
    DateTime OccurredOn { get;}
  }
}