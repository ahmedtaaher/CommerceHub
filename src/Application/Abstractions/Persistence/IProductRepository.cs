using Domain.Catalog.Entities;
using Domain.Catalog.ValueObjects;

namespace Application.Abstractions.Persistence
{
  public interface IProductRepository
  {
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Product?> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);

    Task<bool> ExistsBySkuAsync(Sku sku, CancellationToken cancellationToken = default);

    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    void Update(Product product);

    void Remove(Product product);
  }
}