using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions.Identity;
using Application.Common.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Jwt
{
  public sealed class JwtProvider : IJwtProvider
  {
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
      _options = options.Value;
    }

    public TokenResponse GenerateAsync(Guid userId, string email, IEnumerable<string> roles)
    {
      var claims = new List<Claim>
      {
        new(JwtRegisteredClaimNames.Sub, userId.ToString()),
        new(JwtRegisteredClaimNames.Email, email),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var expiresAt = DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes);

      var token = new JwtSecurityToken(
        issuer: _options.Issuer,
        audience: _options.Audience,
        claims: claims,
        expires: expiresAt,
        signingCredentials: credentials);

      var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse(accessToken, expiresAt);
    }
  }
}