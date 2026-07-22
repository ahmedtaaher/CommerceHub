using Domain.Shared.Errors;

namespace Domain.Catalog.Errors
{
  public static class CatalogErrors
  {
    public static readonly Error EmptyName = new("catalog.empty_name", "Product name is required.");

    public static readonly Error InvalidNameLength = new("catalog.invalid_name_length", "Product name must be between 3 and 200 characters.");

    public static readonly Error InvalidSku = new("catalog.invalid_sku", "SKU is invalid.");

    public static readonly Error EmptySku = new("catalog.empty_sku", "SKU is required.");

    public static readonly Error InvalidSkuLength = new("catalog.invalid_sku_length", "SKU must be between 3 and 50 characters.");

    public static readonly Error ProductNameRequired = new("catalog.product_name_required", "Product name is required.");

    public static readonly Error ProductNameTooShort = new("catalog.product_name_too_short", "Product name must be at least 3 characters.");

    public static readonly Error ProductNameTooLong = new("catalog.product_name_too_long", "Product name cannot exceed 200 characters.");
  }
}