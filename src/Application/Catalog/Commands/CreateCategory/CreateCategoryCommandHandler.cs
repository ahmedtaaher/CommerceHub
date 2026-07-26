using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Domain.Catalog.Entities;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Errors;

namespace Application.Catalog.Commands.CreateCategory
{
  public sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
  {
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
      var nameResult = CategoryName.Create(request.Name);

      if (nameResult.IsFailure)
        return Result<Guid>.Failure(nameResult.Error);

      var exists = await _categoryRepository.ExistsByNameAsync(nameResult.Value, cancellationToken);

      if (exists)
        return Result<Guid>.Failure(CatalogErrors.CategoryAlreadyExists);

      var categoryId = Guid.NewGuid();

      var categoryResult = Category.Create(categoryId, nameResult.Value, request.Description);

      if (categoryResult.IsFailure)
        return Result<Guid>.Failure(categoryResult.Error);

      await _categoryRepository.AddAsync(categoryResult.Value, cancellationToken);

      return Result<Guid>.Success(categoryId);
    }
  }
}