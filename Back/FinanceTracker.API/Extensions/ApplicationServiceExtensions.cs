using FinanceTracker.API.Data;
using FinanceTracker.API.Service;
using FinanceTracker.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
                                                                 IConfiguration configuration)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
