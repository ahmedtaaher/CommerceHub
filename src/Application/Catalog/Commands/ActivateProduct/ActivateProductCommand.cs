using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.ActivateProduct
{
  public sealed record ActivateProductCommand(Guid ProductId) : ICommand;

}