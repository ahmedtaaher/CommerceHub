using Application.Abstractions.Messaging;

namespace Application.Catalog.Commands.CreateCategory
{
  public sealed record CreateCategoryCommand(
    string Name,
    string? Description
  ) : ICommand<Guid>;

}