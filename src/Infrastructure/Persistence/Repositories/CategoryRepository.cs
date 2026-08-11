using Application.Abstractions.Persistence;
using Domain.Catalog.Entities;
using Domain.Catalog.ValueObjects;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly CommerceHubWriteDbContext _context;
    public CategoryRepository(CommerceHubWriteDbContext context)
    {
      _context = context;
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
      await _context.Categories.AddAsync(category, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(CategoryName name, CancellationToken cancellationToken = default)
    {
      return await _context.Categories.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(CategoryName name, CancellationToken cancellationToken = default)
    {
      return await _context.Categories.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public void Remove(Category category)
    {
      _context.Categories.Remove(category);
    }

    public void Update(Category category)
    {
      _context.Categories.Update(category);
    }
  }
}