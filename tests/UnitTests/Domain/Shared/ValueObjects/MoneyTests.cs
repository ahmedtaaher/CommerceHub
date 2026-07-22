using Domain.Shared.ValueObjects;

namespace UnitTests.Domain.Shared.ValueObjects
{
  public class MoneyTests
  {
    [Fact]
    public void Should_Create_Money()
    {
      var money = new Money(100, "USD");

      Assert.Equal(100, money.Amount);
      Assert.Equal("USD", money.Currency);
    }

    [Fact]
    public void Should_Add_Money()
    {
      var first = new Money(100, "USD");
      var second = new Money(50, "USD");

      var result = first + second;

      Assert.Equal(150, result.Amount);
    }

    [Fact]
    public void Should_Be_Equal()
    {
      var first = new Money(100, "USD");
      var second = new Money(100, "USD");

      Assert.Equal(first, second);
    }

    [Fact]
    public void Should_Throw_When_Currencies_Differ()
    {
      var usd = new Money(100, "USD");
      var eur = new Money(50, "EUR");

      Assert.Throws<InvalidOperationException>(() =>
      {
        var result = usd + eur;
      });
    }

    [Fact]
    public void Should_Reject_Negative_Amount()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
      {
        new Money(-10, "USD");
      });
    }
  }
}