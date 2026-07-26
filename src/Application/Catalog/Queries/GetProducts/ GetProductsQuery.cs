using Application.Abstractions.Messaging;
using Application.Common.Models;
using Application.Common.Responses;

namespace Application.Catalog.Queries.GetProducts
{
  public sealed record GetProductsQuery(
    int Page = 1,
    int PageSize = 20,
    string? Search = null,
    string? Sort = null
  ) : IQuery<PagedResult<ProductResponse>>;
}