using FinanceTracker.Infra.Interfaces;
using FinanceTracker.Shared.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.API.Helper
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (context.HttpContext.User.Identity?.IsAuthenticated != true)
                return;
            var username = resultContext.HttpContext.User.GetUsername();
            var userRepository = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return;
            user.LastActive = DateTime.UtcNow;
            await userRepository.SaveAllAsync();
        }
    }
}
