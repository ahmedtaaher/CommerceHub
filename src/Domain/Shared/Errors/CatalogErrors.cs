namespace Domain.Shared.Errors
{
  public static class CatalogErrors
  {
    public static readonly Error InvalidPrice = new("catalog.invalid_price", "Price cannot be negative.");

    public static readonly Error EmptyName = new("catalog.empty_name", "Product name is required.");

    public static readonly Error InvalidSku = new("catalog.invalid_sku", "SKU is invalid.");
  }
}