using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity
{
  public sealed class IdentitySeeder
  {
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
      var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

      string[] roles =
      {
        "Admin",
        "Manager",
        "Employee",
        "Viewer"
      };

      foreach (var role in roles)
      {
        if (!await roleManager.RoleExistsAsync(role))
        {
          await roleManager.CreateAsync(new ApplicationRole
          {
            Name = role
          });
        }
      }
    }

    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
      var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

      const string email = "admin@commercehub.com";
      const string password = "Admin123!";

      var user = await userManager.FindByEmailAsync(email);

      if (user is not null)
        return;

      user = new ApplicationUser
      {
        Id = Guid.NewGuid(),
        Email = email,
        UserName = email,
        FirstName = "System",
        LastName = "Administrator",
        EmailConfirmed = true
      };

      var result = await userManager.CreateAsync(user, password);

      if (!result.Succeeded)
        throw new Exception("Unable to create admin user.");

      await userManager.AddToRoleAsync(user, "Admin");
    }
  }
}