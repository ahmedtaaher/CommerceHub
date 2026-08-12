using Domain.Catalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Catalog.EventHandlers
{
  public sealed class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
  {
    private readonly ILogger<ProductCreatedDomainEventHandler> _logger;
    public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger)
    {
      _logger = logger;
    }
    
    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Product {ProductId} was created.", notification.ProductId);

      return Task.CompletedTask;
    }
  }
}