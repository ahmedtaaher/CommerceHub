using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity
{
  public class CurrentUser : ICurrentUser
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }
    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid UserId
    {
      get
      {
        var value = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.Parse(value!);
      }
    }

    public string Email => User?.FindFirstValue(ClaimTypes.Email) ?? User?.FindFirstValue(JwtRegisteredClaimNames.Email) ?? string.Empty;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
  }
}