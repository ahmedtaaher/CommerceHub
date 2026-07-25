using FluentValidation.Results;

namespace Application.Exceptions
{
  public sealed class ValidationException : Exception
  {
    public ValidationException(IEnumerable<ValidationFailure> failures) : base("One or more validation failures have occurred.")
    {
      Errors = failures.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
  }
}