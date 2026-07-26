using Application.Abstractions.Persistence;
using Application.Common.Models;
using Application.Common.Responses;
using Domain.Catalog.Entities;
using Infrastructure.Persistence.Read;
using Infrastructure.Persistence.Read.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
  public sealed class ProductReadRepository : IProductReadRepository
  {
    private readonly CommerceHubReadDbContext _context;

    public ProductReadRepository(CommerceHubReadDbContext context)
    {
      _context = context;
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _context.Products.AsNoTracking().Where(x => x.Id == id).Select(x => new ProductResponse(
        x.Id,
        x.Name,
        x.Description,
        x.Sku,
        x.Price,
        x.Currency,
        x.Status)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<ProductResponse>> GetPagedAsync(int page, int pageSize, string? search, string? sort, CancellationToken cancellationToken = default)
    {
      IQueryable<ProductReadModel> query = _context.Products.AsNoTracking();

      if (!string.IsNullOrWhiteSpace(search))
      {
        search = search.Trim();

        query = query.Where(x => x.Name.Contains(search) || x.Description.Contains(search) || x.Sku.Contains(search));
      }

      query = sort?.Trim().ToLowerInvariant() switch
      {
        "name" => query.OrderBy(x => x.Name),

        "-name" => query.OrderByDescending(x => x.Name),

        "price" => query.OrderBy(x => x.Price),

        "-price" => query.OrderByDescending(x => x.Price),

        "status" => query.OrderBy(x => x.Status),

        "-status" => query.OrderByDescending(x => x.Status),

        _ => query.OrderBy(x => x.Name)
      };

      var totalCount = await query.CountAsync(cancellationToken);

      var items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new ProductResponse(
        x.Id,
        x.Name,
        x.Description,
        x.Sku,
        x.Price,
        x.Currency,
        x.Status)).ToListAsync(cancellationToken);

      return new PagedResult<ProductResponse>
      {
        Items = items,
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount
      };
    }
  }
}