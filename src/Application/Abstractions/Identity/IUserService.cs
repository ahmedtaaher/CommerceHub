using Domain.Shared.Errors;

namespace Application.Abstractions.Identity
{
  public interface IUserService
  {
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<Guid> CreateUserAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default);  

    Task<(Guid Id, string Email, IList<string> Roles)?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

    Task<(Guid Id, string Email, IList<string> Roles)?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task AddToRoleAsync(Guid userId, string role, CancellationToken cancellationToken = default);

    Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default);

    Task<(Guid Id, string FirstName, string LastName, string Email, IList<string> Roles)?> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result> UpdateProfileAsync(Guid userId, string firstName, string lastName, CancellationToken cancellationToken = default);

    Task<string?> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken = default);

    Task<Result> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);

    Task<string?> GenerateEmailConfirmationTokenAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result> ConfirmEmailAsync(string email, string token, CancellationToken cancellationToken = default);

    Task<bool> IsEmailConfirmedAsync(string email, CancellationToken cancellationToken = default);

    Task<string?> GenerateEmailConfirmationTokenByEmailAsync(string email, CancellationToken cancellationToken = default);
  }
}