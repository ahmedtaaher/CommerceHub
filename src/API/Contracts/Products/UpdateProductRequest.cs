namespace API.Contracts.Products
{
  public sealed record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency
  );
}