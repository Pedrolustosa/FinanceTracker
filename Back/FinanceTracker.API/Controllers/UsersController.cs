using Microsoft.AspNetCore.Mvc;
using FinanceTracker.API.Entities;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.API.Interfaces;
using FinanceTracker.API.DTOs;
using AutoMapper;
using System.Security.Claims;

namespace FinanceTracker.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
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
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (username == null) return BadRequest("No username found");
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null) return BadRequest("Could not find user");
        _mapper.Map(memberUpdateDto, user);
        _userRepository.Update(user);
        if (await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update the user");
    }
}
