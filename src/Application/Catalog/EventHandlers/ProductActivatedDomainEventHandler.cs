using Domain.Catalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Catalog.EventHandlers
{
  public sealed class ProductActivatedDomainEventHandler : INotificationHandler<ProductActivatedDomainEvent>
  {
    private readonly ILogger<ProductActivatedDomainEventHandler> _logger;
    public ProductActivatedDomainEventHandler(ILogger<ProductActivatedDomainEventHandler> logger)
    {
      _logger = logger;
    }
    public Task Handle(ProductActivatedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Product {ProductId} has been activated.", notification.ProductId);
      return Task.CompletedTask;
    }
  }
}