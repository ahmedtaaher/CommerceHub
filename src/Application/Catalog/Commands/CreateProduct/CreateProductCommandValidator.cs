using FluentValidation;

namespace Application.Catalog.Commands.CreateProduct
{
  public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
  {
    public CreateProductCommandValidator()
    {
      RuleFor(x => x.Name).NotEmpty().MaximumLength(200);

      RuleFor(x => x.Description).MaximumLength(2000);

      RuleFor(x => x.Sku).NotEmpty();

      RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

      RuleFor(x => x.Currency).Length(3);

      RuleFor(x => x.CategoryId).NotEmpty();
    }
  }
}