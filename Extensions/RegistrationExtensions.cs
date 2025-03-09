using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentApp.Persistence.DataBase;

namespace StudentApp.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(
            o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo() { Title = "Your API", Version = "v1" });
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                  {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                      Array.Empty<string>()
                  }
            });
            });


    public static IServiceCollection RegisterDatabase(this IServiceCollection services,IConfiguration configuration)
        => services.AddDbContext<AppDbContext>(options => 
                                            options.UseSqlServer(
                                                configuration.GetConnectionString("DataBase")))
                    .AddHostedService<DatabaseMigrationHostedService>();

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder webApplication)
        => webApplication
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
                });
}
