namespace Domain.Shared.Contracts
{
  public interface IAuditable
  {
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
  }
}