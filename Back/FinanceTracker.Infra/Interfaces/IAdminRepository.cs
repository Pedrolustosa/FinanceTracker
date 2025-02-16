using FinanceTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Infra.Interfaces
{
    public interface IAdminRepository
    {
        Task<AppUser> FindByNameAsync(string username);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<IdentityResult> AddToRolesAsync(AppUser user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRolesAsync(AppUser user, IEnumerable<string> roles);
    }
}
