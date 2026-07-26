using Application.Common.Models;
using Application.Common.Responses;

namespace Application.Abstractions.Persistence
{
  public interface ICategoryReadRepository
  {
    Task<CategoryResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<CategoryResponse>> GetPagedAsync(int page, int pageSize, string? search, string? sort, CancellationToken cancellationToken = default);
  }
}