using Application.Abstractions.Persistence;
using Infrastructure.Persistence.Read;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<CommerceHubWriteDbContext>());

    services.AddScoped<IProductRepository, ProductRepository>();
    
    services.AddScoped<IProductReadRepository, ProductReadRepository>();

    return services;
  }
}