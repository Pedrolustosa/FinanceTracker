using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Infra.Data;
using AutoMapper;
using FinanceTracker.Shared.Helper;
using FinanceTracker.Infra.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Infra.Repositories
{
    public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
    {
        private readonly DataContext _context = context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName.ToLower() != userParams.CurrentUsername.ToLower());
            var projectedQuery = query.ProjectTo<AppUser>(_mapper.ConfigurationProvider);
            return await PagedList<AppUser>.CreateAsync(
                projectedQuery, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser?> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(u => u.UserName.ToLower() == username.ToLower())
                .ProjectTo<AppUser>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users
                .AnyAsync(x => x.NormalizedUserName == username.ToUpper());
        }

        public async Task<IdentityResult> CreateUser(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userManager.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.NormalizedUserName == username.ToUpper());
        }

        public async Task<bool> CheckPassword(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
