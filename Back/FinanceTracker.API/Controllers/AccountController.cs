using AutoMapper;
using FinanceTracker.API.Data;
using FinanceTracker.API.DTOs;
using FinanceTracker.API.Entities;
using FinanceTracker.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FinanceTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(DataContext dataContext, ITokenService tokenService, IMapper mapper) : BaseApiController
{
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        try
        {
            if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

            var user = mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;

            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user),
                KnownAs = user.KnownAs
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            return StatusCode(500, "An error occurred while saving changes to the database.");
        }
    }


    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await dataContext.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => 
                          x.UserName == loginDto.Username.ToLower());
        if (user == null) return Unauthorized("Invalid username");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }
        return new UserDto 
        { 
            Username = user.UserName,
            Token = tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
        };
    }

    private async Task<bool> UserExist(string username)
    {
        return await dataContext.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
