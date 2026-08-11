using Domain.Catalog.Entities;
using Domain.Catalog.ValueObjects;

namespace Application.Abstractions.Persistence
{
  public interface ICategoryRepository
  {
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Category?> GetByNameAsync(CategoryName name, CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(CategoryName name, CancellationToken cancellationToken = default);

    Task AddAsync(Category category, CancellationToken cancellationToken = default);

    void Update(Category category);

    void Remove(Category category);
  }
}