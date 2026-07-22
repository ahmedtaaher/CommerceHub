namespace Domain.Shared.Errors
{
  public static class CatalogErrors
  {
    public static readonly Error InvalidSku = new("catalog.invalid_sku", "SKU is invalid.");

    public static readonly Error EmptySku = new("catalog.empty_sku", "SKU is required.");

    public static readonly Error InvalidSkuLength = new("catalog.invalid_sku_length", "SKU must be between 3 and 50 characters.");
  }
}