using FluentValidation;

namespace Application.Catalog.Commands.DeleteCategory
{
  public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
  {
    public DeleteCategoryCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}