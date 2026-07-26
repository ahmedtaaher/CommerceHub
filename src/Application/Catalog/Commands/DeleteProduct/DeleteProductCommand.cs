using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.DeleteProduct
{
  public sealed record DeleteProductCommand(Guid Id) : ICommand;
}