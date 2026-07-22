namespace Domain.Shared.Errors
{
  public static class CommonErrors
  {
    public static readonly Error NegativeAmount = new("common.negative_amount", "Amount cannot be negative.");

    public static readonly Error CurrencyRequired = new("common.currency_required", "Currency is required.");

    public static readonly Error InvalidCurrency = new("common.invalid_currency", "Currency must be a valid ISO-4217 code.");
  }
}