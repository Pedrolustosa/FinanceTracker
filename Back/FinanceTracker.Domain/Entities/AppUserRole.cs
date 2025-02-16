using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Domain.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; } = null;
        public AppRole Role { get; set; } = null;
    }
}
