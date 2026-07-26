using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record CategoryUpdatedDomainEvent(Guid Id) : DomainEvent;
}