using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record ProductDiscontinuedDomainEvent(Guid ProductId) : DomainEvent;
}