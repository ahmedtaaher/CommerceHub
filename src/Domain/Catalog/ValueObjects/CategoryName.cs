using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Catalog.ValueObjects
{
  public sealed class CategoryName : ValueObject
  {
    public const int MaxLength = 100;
    public string Value { get; }

    private CategoryName(string value)
    {
      Value = value;
    }

    public static Result<CategoryName> Create(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        return Result<CategoryName>.Failure(new Error("Category.Name.Empty", "Category name is required."));
      }

      value = value.Trim();

      if (value.Length > MaxLength)
      {
        return Result<CategoryName>.Failure(new Error("Category.Name.TooLong", $"Category name cannot exceed {MaxLength} characters."));
      }

      return Result<CategoryName>.Success(new CategoryName(value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
      yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(CategoryName categoryName) => categoryName.Value;
  }
}