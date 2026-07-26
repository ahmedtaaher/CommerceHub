using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Application.Common.Models;
using Application.Common.Responses;
using Domain.Shared.Errors;

namespace Application.Catalog.Queries.GetProducts
{
  public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PagedResult<ProductResponse>>
  {
    private readonly IProductReadRepository _repository;

    public GetProductsQueryHandler(IProductReadRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result<PagedResult<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
      var products = await _repository.GetPagedAsync(request.Page, request.PageSize, request.Search, request.Sort, cancellationToken);

      return Result<PagedResult<ProductResponse>>.Success(products);
    }
  }
}