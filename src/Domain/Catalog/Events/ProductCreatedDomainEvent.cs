using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record ProductCreatedDomainEvent(Guid ProductId) : DomainEvent;
}