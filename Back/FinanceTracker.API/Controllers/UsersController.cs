using Microsoft.AspNetCore.Mvc;
using FinanceTracker.API.Entities;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.API.Interfaces;
using FinanceTracker.API.DTOs;
using AutoMapper;

namespace FinanceTracker.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    private readonly IUserRepository _userRepository = userRepository;

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
}
