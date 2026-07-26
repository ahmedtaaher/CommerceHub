using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record CategoryDeletedDomainEvent(Guid Id) : DomainEvent;
}