using FinanceTracker.API.Data;
using FinanceTracker.API.Service;
using FinanceTracker.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.API.Helpers;

namespace FinanceTracker.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
                                                                 IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddSignalR();
        return services;
    }
}
