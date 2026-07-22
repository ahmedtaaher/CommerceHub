using Domain.Shared.Errors;

namespace UnitTests.Shared
{
  public class ResultTests
  {
    [Fact]
    public void Success_Should_Return_Success_Result()
    {
      var result = Result.Success();

      Assert.True(result.IsSuccess);
      Assert.False(result.IsFailure);
      Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Failure_Should_Return_Failure_Result()
    {
      var error = new Error("catalog.invalid_price", "Price cannot be negative.");

      var result = Result.Failure(error);

      Assert.False(result.IsSuccess);
      Assert.True(result.IsFailure);
      Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Generic_Success_Should_Return_Value()
    {
      var result = Result<int>.Success(100);

      Assert.True(result.IsSuccess);
      Assert.Equal(100, result.Value);
    }

    [Fact]
    public void Generic_Failure_Should_Return_Error()
    {
      var error = new Error("catalog.invalid_price", "Price cannot be negative.");

      var result = Result<int>.Failure(error);

      Assert.True(result.IsFailure);
      Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Accessing_Value_On_Failure_Should_Throw()
    {
      var result = Result<int>.Failure(new Error("error", "Something went wrong"));

      Assert.Throws<InvalidOperationException>(() =>
      {
        _ = result.Value;
      });
    }

    [Fact]
    public void Creating_Success_Result_With_Error_Should_Throw()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        _ = new FakeResult(true, new Error("error", "invalid"));
      });
    }

    [Fact]
    public void Creating_Failure_Result_Without_Error_Should_Throw()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        _ = new FakeResult(false, Error.None);
      });
    }

    private sealed class FakeResult : Result
    {
      public FakeResult(bool success, Error error): base(success, error)
      {
      }
    }
  }
}