using Microsoft.AspNetCore.Mvc;
using FinanceTracker.API.Entities;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.API.Interfaces;
using FinanceTracker.API.DTOs;
using AutoMapper;
using System.Security.Claims;
using FinanceTracker.API.Extensions;

namespace FinanceTracker.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet("GetUsers")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        try
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("GetUserById/{id:int}")]
    public async Task<ActionResult<AppUser>> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user is null) return NotFound();
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("GetUserByUsername/{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
    {
        try
        {
            var user = await _userRepository.GetMemberAsync(username);
            if (user is null) return NotFound();
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("Could not find user");
        _mapper.Map(memberUpdateDto, user);
        _userRepository.Update(user);
        if (await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update the user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("Cannot update user");
        var result = await photoService.AddPhotoAsync(file);
        if(result.Error is not null) return BadRequest(result.Error.Message);
        var photo = new Photo
        {
            Url = result.SecureUri.AbsoluteUri,
            PublicId = result.PublicId,
        };
        user.Photos.Add(photo);
        if(await _userRepository.SaveAllAsync()) return CreatedAtAction(nameof(GetUserByUsername), 
                                                                        new { username = user.UserName}, 
                                                                        _mapper.Map<PhotoDto>(photo));

        return BadRequest("Problem adding photo/s");
    }
}
