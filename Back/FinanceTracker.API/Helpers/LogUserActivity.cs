﻿using FinanceTracker.API.Extensions;
using FinanceTracker.API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinanceTracker.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (context.HttpContext.User.Identity?.IsAuthenticated != true) return;
            var userId = resultContext.HttpContext.User.GetUserId();
            var unitOfWork = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if(user == null) return;
            user.LastActive = DateTime.UtcNow;
            await unitOfWork.Complete();
        }
    }
}
