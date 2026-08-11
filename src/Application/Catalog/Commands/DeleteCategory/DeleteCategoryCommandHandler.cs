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
     private readonly IProductRepository _productRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository, IProductRepository productRepository)
    {
      _repository = repository;
      _productRepository = productRepository;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
      var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (category is null)
        return Result.Failure(CatalogErrors.CategoryNotFound);

      var hasProducts = await _productRepository.ExistsByCategoryIdAsync(request.Id, cancellationToken);

      if (hasProducts)
        return Result.Failure(CatalogErrors.CategoryHasProducts);

      category.SoftDelete();

      _repository.Update(category);

      return Result.Success();
    }
  }
}