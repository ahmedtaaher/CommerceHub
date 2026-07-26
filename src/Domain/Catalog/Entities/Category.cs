using Domain.Catalog.Events;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Catalog.Entities
{
  public sealed class Category : AggregateRoot<Guid>
  {
    private Category()
    {
      
    }

    public CategoryName Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public static Result<Category> Create(Guid id,CategoryName name, string? description)
    {
      var category = new Category
      {
        Id = id,
        Name = name,
        Description = description?.Trim()
      };

      category.RaiseDomainEvents(new CategoryCreatedDomainEvent(category.Id));

      return Result<Category>.Success(category);
    }

    public Result Rename(CategoryName name)
    {
      ArgumentNullException.ThrowIfNull(name);

      Name = name;

      RaiseDomainEvents(new CategoryUpdatedDomainEvent(Id));

      return Result.Success();
    }

    public Result ChangeDescription(string? description)
    {
      Description = description?.Trim();

      RaiseDomainEvents(new CategoryUpdatedDomainEvent(Id));

      return Result.Success();
    }
  }
}