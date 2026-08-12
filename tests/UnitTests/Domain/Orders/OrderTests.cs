using Domain.Orders.Entities;
using Domain.Orders.Enums;
using Domain.Shared.ValueObjects;

namespace UnitTests.Domain.Orders
{
  public class OrderTests
  {
    [Fact]
    public void Create_Should_Succeed()
    {
      var userId = Guid.NewGuid();
      var currency = Currency.Create("EGP").Value;

      var result = Order.Create(userId, currency);

      Assert.True(result.IsSuccess);
      Assert.NotNull(result.Value);
      Assert.Equal(userId, result.Value.UserId);
      Assert.Empty(result.Value.Items);
    }

    [Fact]
    public void Create_WithEmptyUserId_ShouldFail()
    {
      var currency = Currency.Create("EGP").Value;

      var result = Order.Create(Guid.Empty, currency);

      Assert.True(result.IsFailure);
    }

    [Fact]
    public void AddItem_Should_Succeed()
    {
      var currency = Currency.Create("EGP").Value;

      var order = Order.Create(Guid.NewGuid(), currency).Value;

      var productId = Guid.NewGuid();

      var money = Money.Create(45000m, "EGP").Value;

      var item = OrderItem.Create(productId, "iPhone 16", money, 2).Value;

      var result = order.AddItem(item);

      Assert.True(result.IsSuccess);
      Assert.Single(order.Items);
      Assert.Equal(productId, order.Items[0].ProductId);
      Assert.Equal(2, order.Items[0].Quantity);
    }

    [Fact]
    public void TotalAmount_Should_Return_Sum_Of_Items()
    {
      var currency = Currency.Create("EGP").Value;

      var order = Order.Create(Guid.NewGuid(), currency).Value;

      var item1 = OrderItem.Create(Guid.NewGuid(), "iPhone 16", Money.Create(45000m, "EGP").Value, 2).Value;

      var item2 = OrderItem.Create(Guid.NewGuid(), "AirPods", Money.Create(5000m, "EGP").Value, 1).Value;

      order.AddItem(item1);
      order.AddItem(item2);

      var total = order.TotalAmount;

      Assert.Equal(95000m, total);
    }

    [Fact]
    public void AddItem_AfterConfirmation_ShouldFail()
    {
      var currency = Currency.Create("EGP").Value;

      var order = Order.Create(Guid.NewGuid(), currency).Value;

      var item = OrderItem.Create(Guid.NewGuid(), "iPhone 16", Money.Create(45000m, "EGP").Value, 1).Value;

      order.AddItem(item);

      var confirmResult = order.Confirm();

      Assert.True(confirmResult.IsSuccess);

      var secondItem = OrderItem.Create(Guid.NewGuid(), "AirPods", Money.Create(5000m, "EGP").Value, 1).Value;

      var result = order.AddItem(secondItem);

      Assert.True(result.IsFailure);
    }

    [Fact]
    public void Confirm_EmptyOrder_ShouldFail()
    {
      var currency = Currency.Create("EGP").Value;

      var order = Order.Create(Guid.NewGuid(), currency).Value;

      var result = order.Confirm();

      Assert.True(result.IsFailure);
    }

    [Fact]
    public void Confirm_WithItems_ShouldSucceed()
    {
      var currency = Currency.Create("EGP").Value;

      var order = Order.Create(Guid.NewGuid(), currency).Value;

      var item = OrderItem.Create(Guid.NewGuid(), "Gaming Laptop", Money.Create(60000m, "EGP").Value, 1).Value;

      order.AddItem(item);

      var result = order.Confirm();

      Assert.True(result.IsSuccess);
      Assert.Equal(OrderStatus.Confirmed, order.Status);
    }

    [Fact]
    public void AddItem_WithDifferentCurrency_ShouldFail()
    {
      var orderCurrency = Currency.Create("EGP").Value;

      var order = Order.Create(Guid.NewGuid(), orderCurrency).Value;

      var item = OrderItem.Create(Guid.NewGuid(), "Laptop", Money.Create(1000m, "USD").Value, 1).Value;

      var result = order.AddItem(item);

      Assert.True(result.IsFailure);
    }
  }
}