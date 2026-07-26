using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.DeleteCategory
{
  public sealed record DeleteCategoryCommand(Guid Id) : ICommand;
}