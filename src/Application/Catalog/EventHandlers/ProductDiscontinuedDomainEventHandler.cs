using Domain.Catalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Catalog.EventHandlers
{
  public sealed class ProductDiscontinuedDomainEventHandler : INotificationHandler<ProductDiscontinuedDomainEvent>
  {
    private readonly ILogger<ProductDiscontinuedDomainEventHandler> _logger;
    public ProductDiscontinuedDomainEventHandler(ILogger<ProductDiscontinuedDomainEventHandler> logger)
    {
      _logger = logger;
    }

    public Task Handle(ProductDiscontinuedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Product {ProductId} was discontinued.", notification.ProductId);

      return Task.CompletedTask;
    }
  }
}