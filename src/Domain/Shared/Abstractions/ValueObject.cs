using System.Collections;

namespace Domain.Shared.Abstractions
{
  public abstract class ValueObject
  {
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
      if (obj is null || obj.GetType() != GetType())
        return false;

      var other = (ValueObject)obj;

      return StructuralComparisons.StructuralEqualityComparer.Equals(GetEqualityComponents().ToArray(), other.GetEqualityComponents().ToArray());
    }

    public override int GetHashCode()
    {
      return GetEqualityComponents().Aggregate(1, HashCode.Combine);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

    public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);

  }
}