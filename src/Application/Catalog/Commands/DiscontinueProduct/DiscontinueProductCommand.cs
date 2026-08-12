using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.DiscontinueProduct
{
  public sealed record DiscontinueProductCommand(Guid ProductId) : ICommand;

}