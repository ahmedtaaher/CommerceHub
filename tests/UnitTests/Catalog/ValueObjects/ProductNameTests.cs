using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;

namespace UnitTests.Catalog.ValueObjects
{
  public class ProductNameTests
  {
    [Fact]
    public void Create_Should_Succeed_When_Name_Is_Valid()
    {
      var result = ProductName.Create("Gaming Laptop");

      Assert.True(result.IsSuccess);
      Assert.Equal("Gaming Laptop", result.Value.Value);
    }

    [Fact]
    public void Create_Should_Trim_Name()
    {
      var result = ProductName.Create("   Gaming Laptop   ");

      Assert.True(result.IsSuccess);
      Assert.Equal("Gaming Laptop", result.Value.Value);
    }

    [Fact]
    public void Create_Should_Fail_When_Name_Is_Empty()
    {
      var result = ProductName.Create("");

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductNameRequired, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Name_Is_Too_Short()
    {
      var result = ProductName.Create("AB");

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductNameTooShort, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Name_Is_Too_Long()
    {
      var value = new string('A', 201);

      var result = ProductName.Create(value);

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductNameTooLong, result.Error);
    }

    [Fact]
    public void Equal_Product_Names_Should_Be_Equal()
    {
      var first = ProductName.Create("Laptop").Value;
      var second = ProductName.Create("Laptop").Value;

      Assert.Equal(first, second);
    }
  }
}