using Domain.Catalog.Enums;
using Domain.Catalog.Errors;
using Domain.Catalog.Events;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace Domain.Catalog.Entities
{
  public sealed class Product : AggregateRoot<Guid>
  {
    private Product(Guid id, ProductName name, ProductDescription description, Sku sku, Money price) : base(id)
    {
      ArgumentNullException.ThrowIfNull(name);
      ArgumentNullException.ThrowIfNull(description);
      ArgumentNullException.ThrowIfNull(sku);
      ArgumentNullException.ThrowIfNull(price);

      Name = name;
      Description = description;
      Sku = sku;
      Price = price;
      Status = ProductStatus.Draft;
    }

    public ProductName Name { get; private set; }
    public ProductDescription Description { get; private set; }
    public Sku Sku { get; }
    public Money Price { get; private set; }
    public ProductStatus Status { get; private set; }

    public static Result<Product> Create(Guid id, ProductName name, ProductDescription description, Sku sku, Money price)
    {
      ArgumentNullException.ThrowIfNull(name);
      ArgumentNullException.ThrowIfNull(description);
      ArgumentNullException.ThrowIfNull(sku);
      ArgumentNullException.ThrowIfNull(price);

      var product = new Product(id, name, description, sku, price);

      product.RaiseDomainEvents(new ProductCreatedDomainEvent(product.Id));

      return Result<Product>.Success(product);
    }

    public Result Rename(ProductName name)
    {
      ArgumentNullException.ThrowIfNull(name);

      var result = EnsureNotDiscontinued();

      if (result.IsFailure)
        return result;

      Name = name;

      RaiseDomainEvents(new ProductRenamedDomainEvent(Id, name));

      return Result.Success();
    }

    public Result ChangeDescription(ProductDescription description)
    {
      ArgumentNullException.ThrowIfNull(description);

      var result = EnsureNotDiscontinued();

      if (result.IsFailure)
        return result;

      Description = description;

      return Result.Success();
    }

    public Result ChangePrice(Money price)
    {
      ArgumentNullException.ThrowIfNull(price);

      var result = EnsureNotDiscontinued();

      if (result.IsFailure)
        return result;

      Price = price;

      RaiseDomainEvents(new ProductPriceChangedDomainEvent(Id, price, price.Currency));

      return Result.Success();
    }

    public Result Activate()
    {
      if (Status == ProductStatus.Active)
        return Result.Failure(CatalogErrors.ProductAlreadyActive);

      var result = EnsureNotDiscontinued();

      if (result.IsFailure)
        return result;

      Status = ProductStatus.Active;

      RaiseDomainEvents(new ProductActivatedDomainEvent(Id));

      return Result.Success();
    }

    public Result Deactivate()
    {
      if (Status == ProductStatus.Inactive)
        return Result.Failure(CatalogErrors.ProductAlreadyInactive);

      var result = EnsureNotDiscontinued();

      if (result.IsFailure)
        return result;

      Status = ProductStatus.Inactive;

      return Result.Success();
    }

    public Result Discontinue()
    {
      if (Status == ProductStatus.Discontinued)
        return Result.Success();

      Status = ProductStatus.Discontinued;

      RaiseDomainEvents(new ProductDiscontinuedDomainEvent(Id));

      return Result.Success();
    }

    private Result EnsureNotDiscontinued()
    {
      if (Status == ProductStatus.Discontinued)
        return Result.Failure(CatalogErrors.ProductDiscontinued);

      return Result.Success();
    }
  }
}