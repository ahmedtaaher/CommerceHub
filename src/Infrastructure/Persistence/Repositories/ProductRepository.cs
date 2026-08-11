using Application.Abstractions.Persistence;
using Domain.Catalog.Entities;
using Domain.Catalog.ValueObjects;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly CommerceHubWriteDbContext _context;
    public ProductRepository(CommerceHubWriteDbContext context)
    {
      _context = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
      await _context.Products.AddAsync(product, cancellationToken);
    }

    public async Task<bool> ExistsBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
      return await _context.Products.AnyAsync(x => x.Sku == sku, cancellationToken);
    }

    public async Task<bool> ExistsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
      return await _context.Products.AnyAsync(x => x.CategoryId == categoryId, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Product?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
      return await _context.Products.FirstOrDefaultAsync(x => x.Sku == sku, cancellationToken);
    }

    public void Remove(Product product)
    {
      _context.Products.Remove(product);
    }

    public void Update(Product product)
    {
      _context.Products.Update(product);
    }
  }
}