using Application.Abstractions.Messaging;
using Application.Common.Responses;

namespace Application.Catalog.Queries.GetProductById
{
  public sealed record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
}