using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Shared.ValueObjects
{
  public class Currency : ValueObject
  {
    private Currency(string code)
    {
      Code = code;
    }

    public string Code { get; }

    public static Result<Currency> Create(string code)
    {
      if (string.IsNullOrWhiteSpace(code))
        return Result<Currency>.Failure(CommonErrors.CurrencyRequired);

      code = code.Trim().ToUpperInvariant();

      if (code.Length != 3 || !code.All(char.IsLetter))
        return Result<Currency>.Failure(CommonErrors.InvalidCurrency);

      return Result<Currency>.Success(new Currency(code));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
      yield return Code;
    }

    public override string ToString()
    {
      return Code;
    }
  }
}