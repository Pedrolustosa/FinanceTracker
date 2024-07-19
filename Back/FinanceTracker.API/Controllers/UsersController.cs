using FinanceTracker.API.Data;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinanceTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext dataContext) : BaseApiController
{
    private readonly DataContext _dataContext = dataContext;

    [AllowAnonymous]
    [HttpGet("GetUsers")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        try
        {
            var users = await _dataContext.Users.ToListAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [Authorize]
    [HttpGet("GetUserById/{id:int}")]
    public async Task<ActionResult<AppUser>> GetUserById(int id)
    {
        try
        {
            var user = await _dataContext.Users.FindAsync(id);
            if(user == null) return NotFound();
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
