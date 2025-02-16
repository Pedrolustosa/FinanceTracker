using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Application.Interface;

namespace FinanceTracker.API.Controllers
{
    public class BuggyController(IBuggyService buggyService) : BaseApiController
    {
        private readonly IBuggyService _buggyService = buggyService;

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return _buggyService.GetSecret();
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var user = await _buggyService.GetNotFoundAsync();
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult<string>> GetServerError()
        {
            return await _buggyService.GetServerErrorAsync();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
