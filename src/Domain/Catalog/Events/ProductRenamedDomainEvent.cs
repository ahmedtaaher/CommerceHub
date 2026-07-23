using Domain.Catalog.ValueObjects;
using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record ProductRenamedDomainEvent(Guid ProductId, ProductName NewName) : DomainEvent;
}