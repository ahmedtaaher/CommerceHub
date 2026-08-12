using Domain.Shared.Abstractions;
using Domain.Shared.Errors;
using Domain.Shared.ValueObjects;

namespace Domain.Orders.Entities
{
  public sealed class OrderItem : BaseEntity<Guid>
  {
    private OrderItem()
    {
      
    }

    private OrderItem(Guid id, Guid productId, string productName, Money unitPrice, int quantity) : base(id)
    {
      ProductId = productId;
      ProductName = productName;
      UnitPrice = unitPrice;
      Quantity = quantity;
    }

    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public Money UnitPrice { get; private set; } = default!;
    public int Quantity { get; private set; }
    public decimal Total => UnitPrice.Amount * Quantity;

    public static Result<OrderItem> Create(Guid productId, string productName, Money unitPrice, int quantity)
    {
      if (productId == Guid.Empty)
      {
        return Result<OrderItem>.Failure(new Error("Order.InvalidProduct", "Product is required."));
      }

      if (string.IsNullOrWhiteSpace(productName))
      {
        return Result<OrderItem>.Failure(new Error("Order.InvalidProductName", "Product name is required."));
      }

      ArgumentNullException.ThrowIfNull(unitPrice);

      if (quantity <= 0)
      {
        return Result<OrderItem>.Failure(new Error("Order.InvalidQuantity", "Quantity must be greater than zero."));
      }

      return Result<OrderItem>.Success(new OrderItem(Guid.NewGuid(), productId, productName, unitPrice, quantity));
    }
  }
}