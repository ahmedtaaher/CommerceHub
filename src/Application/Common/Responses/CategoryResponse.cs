namespace Application.Common.Responses
{
  public sealed record CategoryResponse(
    Guid Id,
    string Name,
    string? Description
  );
}