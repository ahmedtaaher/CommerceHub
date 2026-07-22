using Domain.Shared.Abstractions;
using Domain.Shared.Errors;

namespace Domain.Shared.ValueObjects
{
  public sealed class Money : ValueObject
  {
    private Money(decimal amount, Currency currency)
    {
      Amount = amount;
      Currency = currency;
    }

    public decimal Amount { get; }

    public Currency Currency { get; }

    public static Result<Money> Create(decimal amount, string currencyCode)
    {
      if (amount < 0)
        return Result<Money>.Failure(CommonErrors.NegativeAmount);

      var currencyResult = Currency.Create(currencyCode);

      if (currencyResult.IsFailure)
        return Result<Money>.Failure(currencyResult.Error);

      return Result<Money>.Success(new Money(amount, currencyResult.Value));
    }

    public Money Add(Money other)
    {
      EnsureSameCurrency(other);

      return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
      EnsureSameCurrency(other);

      if (Amount - other.Amount < 0)
        throw new InvalidOperationException("Money cannot become negative.");

      return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(int quantity)
    {
      if (quantity < 0)
        throw new ArgumentOutOfRangeException(nameof(quantity));

      return new Money(Amount * quantity, Currency);
    }

    public bool GreaterThan(Money other)
    {
      EnsureSameCurrency(other);

      return Amount > other.Amount;
    }

    public bool LessThan(Money other)
    {
      EnsureSameCurrency(other);

      return Amount < other.Amount;
    }

    public bool GreaterThanOrEqual(Money other)
    {
      EnsureSameCurrency(other);

      return Amount >= other.Amount;
    }

    public bool LessThanOrEqual(Money other)
    {
      EnsureSameCurrency(other);

      return Amount <= other.Amount;
    }

    private void EnsureSameCurrency(Money other)
    {
      if (Currency != other.Currency)
        throw new InvalidOperationException($"Currency mismatch ({Currency} vs {other.Currency}).");
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
      yield return Amount;
      yield return Currency;
    }

    public override string ToString()
    {
      return $"{Amount:F2} {Currency}";
    }
  }
}