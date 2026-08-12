using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.DeactivateProduct
{
  public sealed record DeactivateProductCommand(Guid ProductId) : ICommand;
}