using Domain.Catalog.Entities;
using Domain.Catalog.Enums;
using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.ValueObjects;

namespace UnitTests.Catalog.Entities
{
  public class ProductTests
  {
    private static readonly Guid ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    [Fact]
    public void Create_Should_Succeed()
    {
      var name = ProductName.Create("Gaming Laptop").Value;
      var description = ProductDescription.Create("High-end gaming laptop").Value;
      var sku = Sku.Create("LAPTOP-001").Value;
      var price = Money.Create(2500m, "USD").Value;

      var result = Product.Create(ProductId, name, description, sku, price);

      Assert.True(result.IsSuccess);

      var product = result.Value;

      Assert.Equal(ProductId, product.Id);
      Assert.Equal(name, product.Name);
      Assert.Equal(description, product.Description);
      Assert.Equal(sku, product.Sku);
      Assert.Equal(price, product.Price);
      Assert.Equal(ProductStatus.Draft, product.Status);
    }

    [Fact]
    public void Activate_Should_Change_Status_To_Active()
    {
      var product = CreateProduct();

      var result = product.Activate();

      Assert.True(result.IsSuccess);
      Assert.Equal(ProductStatus.Active, product.Status);
    }

    [Fact]
    public void Activate_Should_Fail_When_Product_Is_Already_Active()
    {
      var product = CreateProduct();
      product.Activate();

      var result = product.Activate();

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductAlreadyActive, result.Error);
    }

    [Fact]
    public void Deactivate_Should_Change_Status_To_Inactive()
    {
      var product = CreateProduct();

      var result = product.Deactivate();

      Assert.True(result.IsSuccess);
      Assert.Equal(ProductStatus.Inactive, product.Status);
    }

    [Fact]
    public void Rename_Should_Update_Product_Name()
    {
      var product = CreateProduct();
      var newName = ProductName.Create("Office Laptop").Value;

      var result = product.Rename(newName);

      Assert.True(result.IsSuccess);
      Assert.Equal(newName, product.Name);
    }

    [Fact]
    public void ChangeDescription_Should_Update_Description()
    {
      var product = CreateProduct();
      var description = ProductDescription.Create("Business laptop").Value;

      var result = product.ChangeDescription(description);

      Assert.True(result.IsSuccess);
      Assert.Equal(description, product.Description);
    }

    [Fact]
    public void ChangePrice_Should_Update_Price()
    {
      var product = CreateProduct();
      var newPrice = Money.Create(3000m, "USD").Value;

      var result = product.ChangePrice(newPrice);

      Assert.True(result.IsSuccess);
      Assert.Equal(newPrice, product.Price);
    }

    [Fact]
    public void Discontinue_Should_Change_Status_To_Discontinued()
    {
      var product = CreateProduct();

      var result = product.Discontinue();

      Assert.True(result.IsSuccess);
      Assert.Equal(ProductStatus.Discontinued, product.Status);
    }

    [Fact]
    public void Rename_Should_Fail_When_Product_Is_Discontinued()
    {
      var product = CreateProduct();
      product.Discontinue();

      var newName = ProductName.Create("New Name").Value;

      var result = product.Rename(newName);

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductDiscontinued, result.Error);
    }

    [Fact]
    public void ChangePrice_Should_Fail_When_Product_Is_Discontinued()
    {
      var product = CreateProduct();
      product.Discontinue();

      var price = Money.Create(5000m, "USD").Value;

      var result = product.ChangePrice(price);

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductDiscontinued, result.Error);
    }

    private static Product CreateProduct()
    {
      return Product.Create(ProductId, ProductName.Create("Gaming Laptop").Value, ProductDescription.Create("High-end gaming laptop").Value, Sku.Create("LAPTOP-001").Value, Money.Create(2500m, "USD").Value).Value;
    }
  }
}