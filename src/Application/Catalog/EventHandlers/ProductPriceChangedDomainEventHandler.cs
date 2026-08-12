using Domain.Catalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Catalog.EventHandlers
{
  public sealed class ProductPriceChangedDomainEventHandler : INotificationHandler<ProductPriceChangedDomainEvent>
  {
    private readonly ILogger<ProductPriceChangedDomainEventHandler> _logger;
    public ProductPriceChangedDomainEventHandler(ILogger<ProductPriceChangedDomainEventHandler> logger)
    {
      _logger = logger;
    }
    public Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Product {ProductId} price was changed to {NewPrice}.", notification.ProductId, notification.NewPrice);

      return Task.CompletedTask;
    }
  }
}