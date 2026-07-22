using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace UnitTests.Domain.Shared.ValueObjects
{
  public class MoneyTests
  {
    [Fact]
    public void Create_Should_Succeed()
    {
      var result = Money.Create(100m, "usd");

      Assert.True(result.IsSuccess);

      Assert.Equal(100m, result.Value.Amount);
      Assert.Equal("USD", result.Value.Currency.Code);
    }

    [Fact]
    public void Create_Should_Fail_For_Negative_Amount()
    {
      var result = Money.Create(-10m, "USD");

      Assert.True(result.IsFailure);
      Assert.Equal(CommonErrors.NegativeAmount, result.Error);
    }

    [Fact]
    public void Add_Should_Return_New_Instance()
    {
      var first = Money.Create(100, "USD").Value;
      var second = Money.Create(50, "USD").Value;

      var result = first.Add(second);

      Assert.Equal(150, result.Amount);
      Assert.Equal("USD", result.Currency.Code);
    }

    [Fact]
    public void Subtract_Should_Return_New_Instance()
    {
      var first = Money.Create(100, "USD").Value;
      var second = Money.Create(25, "USD").Value;

      var result = first.Subtract(second);

      Assert.Equal(75, result.Amount);
    }

    [Fact]
    public void Multiply_Should_Return_New_Instance()
    {
      var money = Money.Create(10, "USD").Value;

      var result = money.Multiply(5);

      Assert.Equal(50, result.Amount);
    }

    [Fact]
    public void Equal_Money_Should_Be_Equal()
    {
      var first = Money.Create(100, "USD").Value;
      var second = Money.Create(100, "usd").Value;

      Assert.Equal(first, second);
    }

    [Fact]
    public void Different_Currencies_Should_Not_Be_Equal()
    {
      var usd = Money.Create(100, "USD").Value;
      var eur = Money.Create(100, "EUR").Value;

      Assert.NotEqual(usd, eur);
    }

    [Fact]
    public void Add_With_Different_Currencies_Should_Throw()
    {
      var usd = Money.Create(100, "USD").Value;
      var eur = Money.Create(50, "EUR").Value;

      Assert.Throws<InvalidOperationException>(() => usd.Add(eur));
    }
  }
}