using Application.Abstractions.Email;

namespace Infrastructure.Email
{
  public sealed class FakeEmailService : IEmailService
  {
    public Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
      Console.WriteLine("===== EMAIL =====");
      Console.WriteLine($"To: {to}");
      Console.WriteLine($"Subject: {subject}");
      Console.WriteLine(body);
      Console.WriteLine("=================");

      return Task.CompletedTask;
    }
  }
}