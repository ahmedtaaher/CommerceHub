using FluentValidation;

namespace Application.Catalog.Commands.UpdateProduct
{
  public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
  {
    public UpdateProductCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();

      RuleFor(x => x.Name).NotEmpty().MaximumLength(200);

      RuleFor(x => x.Description).MaximumLength(2000);

      RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

      RuleFor(x => x.Currency).Length(3);
    }
  }
}