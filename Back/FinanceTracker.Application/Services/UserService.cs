using AutoMapper;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.DTOs;
using FinanceTracker.Domain.Entities;
using Microsoft.AspNetCore.Http;
using FinanceTracker.Infra.Interfaces;
using FinanceTracker.Shared.Helper;

namespace FinanceTracker.Application.Services
{
    public class UserService(IUserRepository userRepository, IPhotoService photoService, ITokenService tokenService, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPhotoService _photoService = photoService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams)
        {
            return await _userRepository.GetMembersAsync(userParams);
        }

        public async Task<AppUser?> GetUserAsync(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        public async Task<bool> UpdateUserAsync(MemberUpdateDto memberUpdateDto, string currentUsername)
        {
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);
            if (user == null) return false;

            _mapper.Map(memberUpdateDto, user);

            return await _userRepository.SaveAllAsync();
        }

        public async Task<PhotoDto> AddPhotoAsync(IFormFile file, string currentUsername)
        {
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);
            if (user == null)
                throw new Exception("Cannot update user");

            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null)
                throw new Exception(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
                photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
                return _mapper.Map<PhotoDto>(photo);

            throw new Exception("Problem adding photo");
        }

        public async Task<bool> SetMainPhotoAsync(int photoId, string currentUsername)
        {
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);
            if (user == null) return false;

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null || photo.IsMain) return false;

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null)
                currentMain.IsMain = false;

            photo.IsMain = true;

            return await _userRepository.SaveAllAsync();
        }

        public async Task<bool> DeletePhotoAsync(int photoId, string currentUsername)
        {
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);
            if (user == null) return false;

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null || photo.IsMain)
                throw new Exception("This photo cannot be deleted");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null)
                    throw new Exception(result.Error.Message);
            }

            user.Photos.Remove(photo);
            return await _userRepository.SaveAllAsync();
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            if (await _userRepository.UserExists(registerDto.Username))
                return null;

            var user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Username.ToLower();

            var result = await _userRepository.CreateUser(user, registerDto.Password);
            if (!result.Succeeded)
                return null;

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsername(loginDto.Username);
            if (user == null)
                return null;

            if (!await _userRepository.CheckPassword(user, loginDto.Password))
                return null;

            return new UserDto
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Token = await _tokenService.CreateToken(user),
                Gender = user.Gender,
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
        }
    }
}
