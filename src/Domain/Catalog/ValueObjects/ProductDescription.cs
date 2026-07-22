using Domain.Catalog.Errors;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Catalog.ValueObjects
{
  public sealed class ProductDescription : ValueObject
  {
    private ProductDescription(string value)
    {
      Value = value;
    }

    public string Value { get; }

    public static Result<ProductDescription> Create(string? value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return Result<ProductDescription>.Success(new ProductDescription(string.Empty));

      value = Normalize(value);

      if (value.Length > 4000)
        return Result<ProductDescription>.Failure(CatalogErrors.ProductDescriptionTooLong);

      return Result<ProductDescription>.Success(new ProductDescription(value));
    }

    private static string Normalize(string value)
    {
      return value.Trim();
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