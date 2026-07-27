using Domain.Shared.Events;

namespace Domain.Catalog.Events
{
  public sealed record ProductCategoryChangedDomainEvent(
    Guid ProductId,
    Guid CategoryId
  ) : DomainEvent;
}