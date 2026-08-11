using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity
{
  public sealed class UserContext : IUserContext
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
      get
      {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user is null)
          return null;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (Guid.TryParse(userId, out var id))
          return id;

        return null;
      }
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;
  }
}