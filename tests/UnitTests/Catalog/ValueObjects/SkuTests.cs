using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;
using Domain.Shared.Errors;

namespace UnitTests.Catalog.ValueObjects
{
  public class SkuTests
  {
    [Fact]
    public void Create_Should_Return_Success_For_Valid_Sku()
    {
      var result = Sku.Create("laptop-001");

      Assert.True(result.IsSuccess);
      Assert.Equal("LAPTOP-001", result.Value.Value);
    }

    [Fact]
    public void Create_Should_Fail_When_Sku_Is_Empty()
    {
      var result = Sku.Create("");

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.EmptySku, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Sku_Is_Too_Short()
    {
      var result = Sku.Create("AB");

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.InvalidSkuLength, result.Error);
    }

    [Fact]
    public void Create_Should_Fail_When_Sku_Has_Invalid_Characters()
    {
      var result = Sku.Create("ABC@123");

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.InvalidSku, result.Error);
    }

    [Fact]
    public void Equal_Skus_Should_Be_Equal()
    {
      var first = Sku.Create("ABC-001").Value;
      var second = Sku.Create("abc-001").Value;

      Assert.Equal(first, second);
    }
  }
}