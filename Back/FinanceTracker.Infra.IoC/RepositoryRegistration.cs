using FinanceTracker.Infra.Interfaces;
using FinanceTracker.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Infra.IoC
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBuggyRepository, BuggyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
