using Domain.Catalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Catalog.EventHandlers
{
  public sealed class ProductRenamedDomainEventHandler : INotificationHandler<ProductRenamedDomainEvent>
  {
    private readonly ILogger<ProductRenamedDomainEventHandler> _logger;
    public ProductRenamedDomainEventHandler(ILogger<ProductRenamedDomainEventHandler> logger)
    {
      _logger = logger;
    }

    public Task Handle(ProductRenamedDomainEvent notification, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Product {ProductId} was renamed to {NewName}.", notification.ProductId, notification.NewName);

      return Task.CompletedTask;
    }
  }
}