using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.API.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}
