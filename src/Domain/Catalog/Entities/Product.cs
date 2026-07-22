using Domain.Catalog.Enums;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace Domain.Catalog.Entities
{
  public sealed class Product : AggregateRoot<Guid>
  {
    private Product(Guid id, string name, string description, Sku sku, Money price) : base(id)
    {
      Name = name;
      Description = description;
      Sku = sku;
      Price = price;
      Status = ProductStatus.Draft;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Sku Sku { get; private set; }

    public Money Price { get; private set; }

    public ProductStatus Status { get; private set; }

    public static Result<Product> Create(string name, string description, Sku sku, Money price)
    {
      if (string.IsNullOrWhiteSpace(name))
        return Result<Product>.Failure(CatalogErrors.EmptyName);

      name = name.Trim();

      if (name.Length < 3 || name.Length > 200)
        return Result<Product>.Failure(CatalogErrors.InvalidNameLength);

      return Result<Product>.Success(new Product(Guid.NewGuid(), name, description.Trim(), sku, price));
    }

    public Result Activate()
    {
      if (Status == ProductStatus.Active)
        return Result.Success();

      Status = ProductStatus.Active;

      return Result.Success();
    }

    public Result Deactivate()
    {
      if (Status == ProductStatus.Inactive)
        return Result.Success();

      Status = ProductStatus.Inactive;

      return Result.Success();
    }

    public Result ChangePrice(Money newPrice)
    {
      Price = newPrice;

      return Result.Success();
    }

    public Result Rename(string newName)
    {
      if (string.IsNullOrWhiteSpace(newName))
        return Result.Failure(CatalogErrors.EmptyName);

      newName = newName.Trim();

      if (newName.Length < 3 || newName.Length > 200)
        return Result.Failure(CatalogErrors.InvalidNameLength);

      Name = newName;

      return Result.Success();
    }
  }
}