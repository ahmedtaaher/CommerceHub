using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record ProductActivatedDomainEvent(Guid ProductId) : DomainEvent;
}