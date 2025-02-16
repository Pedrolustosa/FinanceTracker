using System.Security.Claims;

namespace FinanceTracker.Shared.Helper
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var username = user.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new Exception("Cannot get username from token");
            return username;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("Cannot get user id from token");
            return int.Parse(userIdString);
        }
    }
}
