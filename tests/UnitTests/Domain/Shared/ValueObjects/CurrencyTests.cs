using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace UnitTests.Domain.Shared.ValueObjects
{
  public class CurrencyTests
  {
    [Fact]
    public void Create_Should_Return_Success()
    {
      var result = Currency.Create("usd");

      Assert.True(result.IsSuccess);
      Assert.Equal("USD", result.Value.Code);
    }

    [Fact]
    public void Create_Should_Fail_When_Empty()
    {
      var result = Currency.Create("");

      Assert.True(result.IsFailure);
      Assert.Equal(CommonErrors.CurrencyRequired, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Not_Three_Letters()
    {
      var result = Currency.Create("US");

      Assert.True(result.IsFailure);
      Assert.Equal(CommonErrors.InvalidCurrency, result.Error);
    }

    [Fact]
    public void Equal_Currencies_Should_Be_Equal()
    {
      var first = Currency.Create("USD").Value;
      var second = Currency.Create("usd").Value;

      Assert.Equal(first, second);
    }
  }
}