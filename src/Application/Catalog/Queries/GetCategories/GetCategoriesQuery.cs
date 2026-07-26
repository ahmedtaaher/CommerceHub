using Application.Abstractions.Messaging;
using Application.Common.Models;
using Application.Common.Responses;

namespace Application.Catalog.Queries.GetCategories
{
  public sealed record GetCategoriesQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null,
    string? Sort = null
  ) : IQuery<PagedResult<CategoryResponse>>;

}