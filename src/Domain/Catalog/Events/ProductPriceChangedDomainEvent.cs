using Domain.Shared.Events;
using Domain.Shared.ValueObjects;

namespace Domain.Catalog.Events
{
  public sealed record ProductPriceChangedDomainEvent(Guid ProductId, Money NewPrice, Currency Currency) : DomainEvent;

}