using FinanceTracker.Domain.Entities;
using FinanceTracker.Infra.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Infra.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRolesAsync(AppUser user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(AppUser user, IEnumerable<string> roles)
        {
            return await _userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}
