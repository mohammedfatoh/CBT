using CBT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CBT.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> DeActiveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            user.IsEnabled = false;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception();

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> ActiveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            user.IsEnabled = true;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception();

            return Ok();

        }

    }
}
