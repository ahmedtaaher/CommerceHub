using Application.Abstractions.Messaging;
using Application.Abstractions.Persistence;
using Application.Common.Responses;
using Domain.Catalog.Errors;
using Domain.Shared.Errors;

namespace Application.Catalog.Queries.GetProductById
{
  public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
  {
    private readonly IProductReadRepository _repository;

    public GetProductByIdQueryHandler(IProductReadRepository repository)
    {
      _repository = repository;
    }
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

      if (product is null)
      {
        return Result<ProductResponse>.Failure(CatalogErrors.ProductNotFound);
      }

      return Result<ProductResponse>.Success(product);
    }
  }
}