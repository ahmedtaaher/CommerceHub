using Domain.Catalog.Errors;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Catalog.ValueObjects
{
  public sealed class ProductName : ValueObject
  {
    private ProductName(string value)
    {
      Value = value;
    }

    public string Value { get; }

    public static Result<ProductName> Create(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return Result<ProductName>.Failure(CatalogErrors.ProductNameRequired);

      value = value.Trim();

      if (value.Length < 3)
        return Result<ProductName>.Failure(CatalogErrors.ProductNameTooShort);

      if (value.Length > 200)
        return Result<ProductName>.Failure(CatalogErrors.ProductNameTooLong);

      return Result<ProductName>.Success(new ProductName(value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
      yield return Value;
    }

    public override string ToString()
    {
      return Value;
    }
  }
}