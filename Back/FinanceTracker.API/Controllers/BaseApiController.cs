using FinanceTracker.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase{ }
