using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace UnitTests.Domain.Shared.ValueObjects
{
  public class MoneyTests
  {
    [Fact]
    public void Create_Should_Return_Success_When_Data_Is_Valid()
    {
      var result = Money.Create(100, "usd");

      Assert.True(result.IsSuccess);
      Assert.Equal(100, result.Value.Amount);
      Assert.Equal("USD", result.Value.Currency);
    }

    [Fact]
    public void Create_Should_Fail_When_Amount_Is_Negative()
    {
      var result = Money.Create(-10, "USD");

      Assert.True(result.IsFailure);
      Assert.Equal(CommonErrors.NegativeAmount, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Currency_Is_Empty()
    {
      var result = Money.Create(100, "");

      Assert.True(result.IsFailure);
      Assert.Equal(CommonErrors.CurrencyRequired, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Currency_Is_Invalid()
    {
      var result = Money.Create(100, "US");

      Assert.True(result.IsFailure);
      Assert.Equal(CommonErrors.InvalidCurrency, result.Error);
    }

    [Fact]
    public void Add_Should_Return_New_Money()
    {
      var first = Money.Create(100, "USD").Value;
      var second = Money.Create(50, "USD").Value;

      var result = first.Add(second);

      Assert.Equal(150, result.Amount);
    }

    [Fact]
    public void Subtract_Should_Return_New_Money()
    {
      var first = Money.Create(100, "USD").Value;
      var second = Money.Create(30, "USD").Value;

      var result = first.Subtract(second);

      Assert.Equal(70, result.Amount);
    }

    [Fact]
    public void Multiply_Should_Return_New_Money()
    {
      var money = Money.Create(100, "USD").Value;

      var result = money.Multiply(5);

      Assert.Equal(500, result.Amount);
    }

    [Fact]
    public void Equal_Money_Should_Be_Equal()
    {
      var first = Money.Create(100, "USD").Value;
      var second = Money.Create(100, "USD").Value;

      Assert.Equal(first, second);
    }
  }
}