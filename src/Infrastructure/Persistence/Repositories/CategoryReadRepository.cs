using Application.Abstractions.Persistence;
using Application.Common.Models;
using Application.Common.Responses;
using Infrastructure.Persistence.Read;
using Infrastructure.Persistence.Read.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
  public class CategoryReadRepository : ICategoryReadRepository
  {
    private readonly CommerceHubReadDbContext _context;

    public CategoryReadRepository(CommerceHubReadDbContext context)
    {
      _context = context;
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _context.Categories.AsNoTracking().Where(x => x.Id == id).Select(x => new CategoryResponse(
        x.Id,
        x.Name,
        x.Description)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<CategoryResponse>> GetPagedAsync(int page, int pageSize, string? search, string? sort, CancellationToken cancellationToken = default)
    {
      IQueryable<CategoryReadModel> query = _context.Categories.AsNoTracking();

      if (!string.IsNullOrWhiteSpace(search))
      {
        search = search.Trim();

        query = query.Where(x => x.Name.Contains(search) || (x.Description != null && x.Description.Contains(search)));
      }

      query = sort?.Trim().ToLowerInvariant() switch
      {
        "name" => query.OrderBy(x => x.Name),

        "-name" => query.OrderByDescending(x => x.Name),

        _ => query.OrderBy(x => x.Name)
      };

      var totalCount = await query.CountAsync(cancellationToken);

      var items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new CategoryResponse(
        x.Id,
        x.Name,
        x.Description)).ToListAsync(cancellationToken);

      return new PagedResult<CategoryResponse>
      {
        Items = items,
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount
      };
    }
  }
}