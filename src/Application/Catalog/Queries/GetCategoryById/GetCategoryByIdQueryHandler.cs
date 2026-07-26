using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Application.Common.Responses;
using Domain.Catalog.Errors;
using Domain.Shared.Errors;

namespace Application.Catalog.Queries.GetCategoryById
{
  public sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
  {
    private readonly ICategoryReadRepository _repository;

    public GetCategoryByIdQueryHandler(ICategoryReadRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
      var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (category is null)
        return Result<CategoryResponse>.Failure(CatalogErrors.CategoryNotFound);

      return Result<CategoryResponse>.Success(category);
    }
  }
}