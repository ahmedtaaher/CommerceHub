namespace Infrastructure.Persistence.Read.Models
{
  public sealed class ProductReadModel
  {
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string Sku { get; init; } = default!;

    public decimal Price { get; init; }

    public string Currency { get; init; } = default!;

    public string Status { get; init; } = default!;

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = default!;
  }
}