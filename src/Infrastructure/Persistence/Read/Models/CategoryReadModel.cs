namespace Infrastructure.Persistence.Read.Models
{
  public sealed class CategoryReadModel
  {
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}