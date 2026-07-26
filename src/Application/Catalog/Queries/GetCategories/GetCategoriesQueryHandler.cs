using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Application.Common.Models;
using Application.Common.Responses;
using Domain.Shared.Errors;

namespace Application.Catalog.Queries.GetCategories
{
  public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
  {
    private readonly ICategoryReadRepository _repository;

    public GetCategoriesQueryHandler(ICategoryReadRepository repository)
    {
      _repository = repository;
    }
    public async Task<Result<PagedResult<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
      var result = await _repository.GetPagedAsync(request.Page, request.PageSize, request.Search, request.Sort, cancellationToken);

      return Result<PagedResult<CategoryResponse>>.Success(result);
    }
  }
}