using System.Text.RegularExpressions;
using Domain.Catalog.Errors;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Catalog.ValueObjects
{
  public class Sku : ValueObject
  {
    private static readonly Regex Regex = new("^[A-Z0-9_-]+$", RegexOptions.Compiled);

    private Sku(string value)
    {
      Value = value;
    }

    public string Value { get; }

    public static Result<Sku> Create(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return Result<Sku>.Failure(CatalogErrors.EmptySku);

      value = value.Trim().ToUpperInvariant();

      if (value.Length < 3 || value.Length > 50)
        return Result<Sku>.Failure(CatalogErrors.InvalidSkuLength);

      if (!Regex.IsMatch(value))
        return Result<Sku>.Failure(CatalogErrors.InvalidSku);

      return Result<Sku>.Success(new Sku(value));
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