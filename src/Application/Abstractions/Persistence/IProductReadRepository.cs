using Application.Common.Models;
using Application.Common.Responses;

namespace Application.Abstractions.Persistence
{
  public interface IProductReadRepository
  {
    Task<ProductResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<ProductResponse>> GetPagedAsync(int page, int pageSize, string? search, string? sort, CancellationToken cancellationToken = default);
  }
}