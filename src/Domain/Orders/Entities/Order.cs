using Domain.Orders.Enums;
using Domain.Shared.Abstractions;
using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace Domain.Orders.Entities
{
  public sealed class Order : AggregateRoot<Guid>
  {
    private readonly List<OrderItem> _items = [];

    private Order()
    {
      
    }

    private Order(Guid id, Guid userId, Currency currency) : base(id)
    {
      UserId = userId;
      Currency = currency;
      Status = OrderStatus.Pending;
    }

    public Guid UserId { get; private set; }

    public OrderStatus Status { get; private set; }

    public Currency Currency { get; private set; } = default!;

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalAmount => _items.Sum(x => x.Total);

    public static Result<Order> Create(Guid userId, Currency currency)
    {
      if (userId == Guid.Empty)
      {
        return Result<Order>.Failure(new Error("Order.InvalidUser", "User is required."));
      }

      ArgumentNullException.ThrowIfNull(currency);

      var order = new Order(Guid.NewGuid(),userId, currency);

      return Result<Order>.Success(order);
    }

    public Result AddItem(OrderItem item)
    {
      ArgumentNullException.ThrowIfNull(item);

      if (Status != OrderStatus.Pending)
      {
        return Result.Failure(new Error("Order.InvalidStatus", "Items can only be added to a pending order."));
      }

      if (item.UnitPrice.Currency != Currency)
      {
        return Result.Failure(new Error("Order.CurrencyMismatch", "Order item currency must match the order currency."));
      }

      _items.Add(item);

      return Result.Success();
    }

    public Result Confirm()
    {
      if (Status != OrderStatus.Pending)
      {
        return Result.Failure(new Error("Order.InvalidStatus", "Only pending orders can be confirmed."));
      }

      if (_items.Count == 0)
      {
        return Result.Failure(new Error("Order.Empty", "An order must contain at least one item."));
      }

      Status = OrderStatus.Confirmed;

      return Result.Success();
    }
  }
}