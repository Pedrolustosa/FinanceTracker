using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FinanceTracker.Infra.IoC
{
    public static class SwaggerRegistration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FinanceTracker.API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Lustosa",
                        Email = "pedroeternalss@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/pedrolustosaengineer/")
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = @"JWT Authorization header using Bearer.
                                    Enter 'Bearer' [space] then put in your token.
                                    Example: 'Bearer 12345abcdef'"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
