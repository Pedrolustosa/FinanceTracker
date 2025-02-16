using FinanceTracker.Domain.Entities;
using FinanceTracker.Shared.Helper;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Infra.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams);
        Task<AppUser?> GetMemberAsync(string username);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<bool> SaveAllAsync();
        Task<bool> UserExists(string username);
        Task<IdentityResult> CreateUser(AppUser user, string password);
        Task<AppUser> GetUserByUsername(string username);
        Task<bool> CheckPassword(AppUser user, string password);
    }
}
