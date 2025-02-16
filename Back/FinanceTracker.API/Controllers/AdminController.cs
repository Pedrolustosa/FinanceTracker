using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Application.Interface;

namespace FinanceTracker.API.Controllers
{
    public class AdminController(IAdminService adminService) : BaseApiController
    {
        private readonly IAdminService _adminService = adminService;

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, string roles)
        {
            if (string.IsNullOrEmpty(roles))
                return BadRequest("You must select at least one role");

            var updatedRoles = await _adminService.EditRoles(username, roles);
            if (updatedRoles == null)
                return BadRequest("User not found or error updating roles");

            return Ok(updatedRoles);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public ActionResult GetPhotosForModeration()
        {
            return Ok("Admin or moderators can see this");
        }
    }
}
