using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.CreateProduct
{
  public sealed record CreateProductCommand(
    string Name,
    string Description,
    string Sku,
    decimal Price,
    string Currency
  ): ICommand<Guid>;
}