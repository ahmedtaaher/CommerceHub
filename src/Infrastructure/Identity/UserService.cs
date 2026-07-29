using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
  public sealed class UserService : IUserService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public async Task<Guid> CreateUserAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default)
    {
      var user = new ApplicationUser
      {
        Id = Guid.NewGuid(),
        UserName = email,
        Email = email,
        FirstName = firstName,
        LastName = lastName
      };

      var result = await _userManager.CreateAsync(user, password);

      if (!result.Succeeded)
      {
        throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
      }

      return user.Id;
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
      var user = await _userManager.FindByEmailAsync(email);

      return user is not null;
    }

    public async Task<(Guid Id, string Email, IList<string> Roles)?> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
      var user = await _userManager.FindByEmailAsync(email);

      if (user is null)
        return null;

      var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

      if (!result.Succeeded)
        return null;

      var roles = await _userManager.GetRolesAsync(user);

      return (user.Id, user.Email!, roles);
    }

    public async Task AddToRoleAsync(Guid userId, string role, CancellationToken cancellationToken = default)
    {
      var user = await _userManager.FindByIdAsync(userId.ToString());

      if (user is null)
        throw new InvalidOperationException("User not found.");

      await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<(Guid Id, string Email, IList<string> Roles)?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
      var user = await _userManager.FindByIdAsync(userId.ToString());

      if (user is null)
        return null;

      var roles = await _userManager.GetRolesAsync(user);

      return (user.Id, user.Email!, roles);
    }
  }
}