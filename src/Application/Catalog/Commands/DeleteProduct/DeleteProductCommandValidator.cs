using FluentValidation;

namespace Application.Catalog.Commands.DeleteProduct
{
  public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
  {
    public DeleteProductCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}