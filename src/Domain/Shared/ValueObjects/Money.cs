using Domain.Shared.Abstractions;

namespace Domain.Shared.ValueObjects
{
  public sealed class Money : ValueObject
  {
    public decimal Amount { get; }

    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
      if (amount < 0)
        throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

      if (string.IsNullOrWhiteSpace(currency))
        throw new ArgumentException("Currency is required.", nameof(currency));

      currency = currency.Trim().ToUpperInvariant();

      if (currency.Length != 3)
        throw new ArgumentException("Currency must be a valid ISO-4217 code.", nameof(currency));

      Amount = amount;
      Currency = currency;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
      yield return Amount;
      yield return Currency;
    }

    private static void EnsureSameCurrency(Money left, Money right)
    {
      if (left.Currency != right.Currency)
        throw new InvalidOperationException($"Cannot operate on different currencies ({left.Currency} and {right.Currency}).");
    }

    public static Money operator +(Money left, Money right)
    {
      EnsureSameCurrency(left, right);

      return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
      EnsureSameCurrency(left, right);

      return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money money, int quantity)
    {
      if (quantity < 0)
        throw new ArgumentOutOfRangeException(nameof(quantity));

      return new Money(money.Amount * quantity, money.Currency);
    }

    public static bool operator >(Money left, Money right)
    {
      EnsureSameCurrency(left, right);

      return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
      EnsureSameCurrency(left, right);

      return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
      EnsureSameCurrency(left, right);

      return left.Amount >= right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
      EnsureSameCurrency(left, right);

      return left.Amount <= right.Amount;
    }

    public override string ToString()
    {
      return $"{Amount:F2} {Currency}";
    }
  }
}