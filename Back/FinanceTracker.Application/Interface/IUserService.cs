using FinanceTracker.Application.DTOs;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Shared.Helper;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams);
        Task<AppUser?> GetUserAsync(string username);
        Task<bool> UpdateUserAsync(MemberUpdateDto memberUpdateDto, string currentUsername);
        Task<PhotoDto> AddPhotoAsync(IFormFile file, string currentUsername);
        Task<bool> SetMainPhotoAsync(int photoId, string currentUsername);
        Task<bool> DeletePhotoAsync(int photoId, string currentUsername);
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginDto);
    }
}
