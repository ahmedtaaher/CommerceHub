using Domain.Shared.Errors;

namespace UnitTests.Shared
{
  public class ErrorTests
  {
    [Fact]
    public void Errors_With_Same_Code_And_Description_Should_Be_Equal()
    {
      var first = new Error("catalog.invalid_price", "Price cannot be negative.");

      var second = new Error("catalog.invalid_price", "Price cannot be negative.");

      Assert.Equal(first, second);
    }

    [Fact]
    public void None_Should_Have_Empty_Code_And_Description()
    {
      Assert.Equal(string.Empty, Error.None.Code);
      Assert.Equal(string.Empty, Error.None.Description);
    }
  }
}