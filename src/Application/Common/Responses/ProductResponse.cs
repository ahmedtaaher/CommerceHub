namespace Application.Common.Responses
{
  public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    string Sku,
    decimal Price,
    string Currency,
    string Status
  );
}