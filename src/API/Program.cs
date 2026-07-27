using Application;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "CommerceHub API",
    Version = "v1"
  });

  var jwtSecurityScheme = new OpenApiSecurityScheme
  {
    Scheme = "bearer",
    BearerFormat = "JWT",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Description = "Enter: Bearer {your JWT token}",

    Reference = new OpenApiReference
    {
      Id = "Bearer",
      Type = ReferenceType.SecurityScheme
    }
  };

  options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      jwtSecurityScheme,
      Array.Empty<string>()
    }
  });
});

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
  await IdentitySeeder.SeedAsync(scope.ServiceProvider);

  await IdentitySeeder.SeedAdminAsync(scope.ServiceProvider);
}

app.Run();