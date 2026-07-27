namespace API.Contracts.Products
{
  public sealed record CreateProductRequest(
    Guid CategoryId,
    string Name,
    string Description,
    string Sku,
    decimal Price,
    string Currency
  );

}