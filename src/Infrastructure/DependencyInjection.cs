using System.Security.Claims;
using System.Text;
using Application.Abstractions.Identity;
using Application.Abstractions.Persistence;
using Infrastructure.Identity;
using Infrastructure.Identity.Jwt;
using Infrastructure.Identity.RefreshTokens;
using Infrastructure.Persistence.Read;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Write;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<CommerceHubWriteDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

    services.AddDbContext<CommerceHubReadDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

    services.AddDbContext<CommerceHubIdentityDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

    services.AddIdentityCore<ApplicationUser>(options =>
    {
      options.User.RequireUniqueEmail = true;

      options.Password.RequiredLength = 8;
      options.Password.RequireDigit = true;
      options.Password.RequireUppercase = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireNonAlphanumeric = false;
    }).AddRoles<ApplicationRole>().AddEntityFrameworkStores<CommerceHubIdentityDbContext>().AddSignInManager<SignInManager<ApplicationUser>>().AddRoleManager<RoleManager<ApplicationRole>>().AddDefaultTokenProviders();

    services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

    services.AddAuthentication(options => 
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options => 
    {
      var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

        NameClaimType = ClaimTypes.NameIdentifier,
        RoleClaimType = ClaimTypes.Role
      };
    });

    services.AddAuthorization();

    services.AddScoped<IJwtProvider, JwtProvider>();

    services.AddScoped<IUserService, UserService>();

    services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<CommerceHubWriteDbContext>());

    services.AddScoped<IProductRepository, ProductRepository>();
    
    services.AddScoped<IProductReadRepository, ProductReadRepository>();

    services.AddScoped<ICategoryRepository, CategoryRepository>();

    services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();

    services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

    services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

    services.AddScoped<ITokenHasher, Sha256TokenHasher>();

    services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

    return services;
  }
}