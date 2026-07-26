using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.UpdateProduct
{
  public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency
  ) : ICommand;
}