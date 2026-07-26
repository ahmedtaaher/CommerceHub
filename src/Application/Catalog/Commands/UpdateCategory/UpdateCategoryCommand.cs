using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.UpdateCategory
{
  public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description
  ) : ICommand;
}