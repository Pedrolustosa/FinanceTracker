using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.API.Extensions;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.DTOs;
using FinanceTracker.Shared.Helper;

namespace FinanceTracker.API.Controllers
{
    [Authorize]
    public class UsersController(IUserService userService) : BaseApiController
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            userParams.CurrentUsername = User.GetUsername();
            var users = await _userService.GetUsersAsync(userParams);

            Response.AddPaginationHeader(users);

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userService.GetUserAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var currentUsername = User.GetUsername();
            if (await _userService.UpdateUserAsync(memberUpdateDto, currentUsername))
                return NoContent();

            return BadRequest("Failed to update the user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            try
            {
                var currentUsername = User.GetUsername();
                var photoDto = await _userService.AddPhotoAsync(file, currentUsername);

                return CreatedAtAction(nameof(GetUser), new { username = currentUsername }, photoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var currentUsername = User.GetUsername();
            if (await _userService.SetMainPhotoAsync(photoId, currentUsername))
                return NoContent();

            return BadRequest("Problem setting main photo");
        }

        [HttpDelete("delete-photo/{photoId:int}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            try
            {
                var currentUsername = User.GetUsername();
                if (await _userService.DeletePhotoAsync(photoId, currentUsername))
                    return Ok();

                return BadRequest("Problem deleting photo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await _userService.Register(registerDto);
            if (userDto == null)
                return BadRequest("Username is taken or registration failed");

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _userService.Login(loginDto);
            if (userDto == null)
                return Unauthorized("Invalid credentials");

            return Ok(userDto);
        }
    }
}
