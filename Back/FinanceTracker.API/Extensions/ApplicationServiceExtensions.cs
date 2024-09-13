using FinanceTracker.API.Data;
using FinanceTracker.API.Service;
using FinanceTracker.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.API.Helpers;

namespace FinanceTracker.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
                                                                 IConfiguration configuration)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt => { opt.UseSqlite(configuration.GetConnectionString("DefaultConnection")); });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
