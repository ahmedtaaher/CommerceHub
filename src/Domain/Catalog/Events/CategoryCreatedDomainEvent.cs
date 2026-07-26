using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record CategoryCreatedDomainEvent(Guid CategoryId) : DomainEvent;
}