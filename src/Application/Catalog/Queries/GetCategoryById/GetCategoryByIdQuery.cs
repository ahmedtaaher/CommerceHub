using Application.Abstractions.Messaging;
using Application.Common.Responses;

namespace Application.Catalog.Queries.GetCategoryById
{
  public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryResponse>;
}