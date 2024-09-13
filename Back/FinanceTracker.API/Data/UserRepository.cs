using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.API.DTOs;
using FinanceTracker.API.Entities;
using FinanceTracker.API.Helpers;
using FinanceTracker.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.Data;

public class UserRepository(DataContext dataContext, IMapper mapper) : IUserRepository
{
    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await dataContext.Users.Where(x => x.UserName == username)
                                      .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                                      .SingleOrDefaultAsync();
    }

    public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
    {
        var query = dataContext.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider);
        return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await dataContext.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await dataContext.Users.Include(x => x.Photos)
                                      .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await dataContext.Users.Include(x => x.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await dataContext.SaveChangesAsync() > 0;
    }

    public async void Update(AppUser user)
    {
        dataContext.Entry(user).State = EntityState.Modified;
    }
}
