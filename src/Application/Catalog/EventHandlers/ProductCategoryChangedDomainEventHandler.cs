using Domain.Catalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Catalog.EventHandlers
{
  public sealed class ProductCategoryChangedDomainEventHandler : INotificationHandler<ProductCategoryChangedDomainEvent>
  {
    private readonly ILogger<ProductCategoryChangedDomainEventHandler> _logger;
    public ProductCategoryChangedDomainEventHandler(ILogger<ProductCategoryChangedDomainEventHandler> logger)
    {
      _logger = logger;
    }
    public Task Handle(ProductCategoryChangedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Product {ProductId} category was changed to {NewCategory}.", notification.ProductId, notification);

      return Task.CompletedTask;
    }
  }
}