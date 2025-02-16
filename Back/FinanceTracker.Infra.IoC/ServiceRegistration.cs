using FinanceTracker.Application.Interface;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.Mappings;
using FinanceTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Infra.IoC
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IBuggyService, BuggyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            return services;
        }
    }
}
