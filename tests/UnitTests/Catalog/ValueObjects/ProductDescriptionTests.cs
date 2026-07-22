using Domain.Catalog.Errors;
using Domain.Catalog.ValueObjects;

namespace UnitTests.Catalog.ValueObjects
{
  public class ProductDescriptionTests
  {
    [Fact]
    public void Create_Should_Succeed_With_Normal_Description()
    {
      var result = ProductDescription.Create("Gaming laptop");

      Assert.True(result.IsSuccess);
      Assert.Equal("Gaming laptop", result.Value.Value);
    }

    [Fact]
    public void Create_Should_Trim_Description()
    {
      var result = ProductDescription.Create("   Gaming laptop   ");

      Assert.True(result.IsSuccess);
      Assert.Equal("Gaming laptop", result.Value.Value);
    }

    [Fact]
    public void Create_Should_Return_Empty_When_Null()
    {
      var result = ProductDescription.Create(null);

      Assert.True(result.IsSuccess);
      Assert.Equal(string.Empty, result.Value.Value);
    }

    [Fact]
    public void Create_Should_Return_Empty_When_Whitespace()
    {
      var result = ProductDescription.Create("   ");

      Assert.True(result.IsSuccess);
      Assert.Equal(string.Empty, result.Value.Value);
    }

    [Fact]
    public void Create_Should_Fail_When_Too_Long()
    {
      var text = new string('A', 4001);

      var result = ProductDescription.Create(text);

      Assert.True(result.IsFailure);
      Assert.Equal(CatalogErrors.ProductDescriptionTooLong, result.Error);
    }

    [Fact]
    public void Equal_Descriptions_Should_Be_Equal()
    {
      var first = ProductDescription.Create("Gaming").Value;
      var second = ProductDescription.Create("Gaming").Value;

      Assert.Equal(first, second);
    }
  }
}