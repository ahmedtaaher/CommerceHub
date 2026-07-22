using Domain.Catalog.Entities;
using Domain.Catalog.Enums;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.ValueObjects;

namespace UnitTests.Catalog.Entities
{
  public class ProductTests
  {
    [Fact]
    public void Create_Should_Succeed()
    {
      var sku = Sku.Create("LAPTOP-001").Value;
      var price = Money.Create(1500m, "USD").Value;

      var result = Product.Create("Gaming Laptop", "High-end gaming laptop", sku, price);

      Assert.True(result.IsSuccess);
      Assert.Equal(ProductStatus.Draft, result.Value.Status);
    }

    [Fact]
    public void Create_Should_Fail_When_Name_Is_Empty()
    {
      var sku = Sku.Create("ABC-001").Value;
      var price = Money.Create(100m, "USD").Value;

      var result = Product.Create("", "", sku, price);

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.EmptyName, result.Error);
    }

    [Fact]
    public void Activate_Should_Change_Status()
    {
      var product = Product.Create("Laptop", "", Sku.Create("ABC-001").Value, Money.Create(100, "USD").Value).Value;

      product.Activate();

      Assert.Equal(ProductStatus.Active, product.Status);
    }

    [Fact]
    public void ChangePrice_Should_Update_Price()
    {
      var product = Product.Create("Laptop", "", Sku.Create("ABC-001").Value, Money.Create(100, "USD").Value).Value;

      product.ChangePrice(Money.Create(250, "USD").Value);

      Assert.Equal(250, product.Price.Amount);
    }
  }
}