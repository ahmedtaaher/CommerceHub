using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.UpdateCategory
{
  public sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
  {
    private readonly ICategoryRepository _repository;

    public UpdateCategoryCommandHandler(ICategoryRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
      var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (category is null)
        return Result.Failure(CatalogErrors.CategoryNotFound);

      var nameResult = CategoryName.Create(request.Name);

      if (nameResult.IsFailure)
        return Result.Failure(nameResult.Error);

      var exists = await _repository.GetByNameAsync(nameResult.Value, cancellationToken);

      if (exists is not null && exists.Id != category.Id)
      {
        return Result.Failure(CatalogErrors.CategoryAlreadyExists);
      }

      var renameResult = category.Rename(nameResult.Value);

      if (renameResult.IsFailure)
        return renameResult;

      var descriptionResult = category.ChangeDescription(request.Description);

      if (descriptionResult.IsFailure)
        return descriptionResult;

      return Result.Success();
    }
  }
}