using FluentValidation;

namespace Application.Catalog.Commands.UpdateCategory
{
  public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
  {
    public UpdateCategoryCommandValidator()
    {
      RuleFor(x => x.Name).NotEmpty().MaximumLength(100);

      RuleFor(x => x.Description).MaximumLength(500);
    }
  }
}