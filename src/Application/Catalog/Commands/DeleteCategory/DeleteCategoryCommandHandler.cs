using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Catalog.Events;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.DeleteCategory
{
  public sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
  {
    private readonly ICategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
      var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (category is null)
        return Result.Failure(CatalogErrors.CategoryNotFound);

      _repository.Remove(category);

      return Result.Success();
    }
  }
}