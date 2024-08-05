using FinanceTracker.API.Data;
using FinanceTracker.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers;

public class BuggyController(DataContext dataContext) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = dataContext.Users.Find(-1);
        if (thing is null) return NotFound();
        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        try
        {
            var thing = dataContext.Users.Find(-1) ?? throw new Exception("A bad thing has happened");
            return thing;
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Computer says no!");
        }
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
}
