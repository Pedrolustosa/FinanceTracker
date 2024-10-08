﻿using FinanceTracker.API.Data;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FinanceTracker.API.Controllers;

public class BuggyController(DataContext context) : BaseApiController
{
    private readonly DataContext _context = context;

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = _context.Users.Find(-1);
        if (thing == null) return NotFound();
        return Ok(thing);
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.Users.Find(-1);
        var thingToReturn = thing.ToString();
        return thingToReturn;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest();
    }
}
