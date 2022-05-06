using CBT.Models;
using CBT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace CBT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                IsEnabled = user.IsEnabled,
                Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult()
            }).ToListAsync();

            return View(users);
        }

        
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            

            if (user == null)
                return NotFound();

           

            var viewModel = new ProfileFormViewModel
            {
                Id = user.Id,
                FullName=user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Age = user.Age,
                Gendre = user.Gendre
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if(userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "This Email is already assigned to another User");
                return View(model);
            }

            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);

            if (userWithSameUserName != null && userWithSameUserName.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "This UserName is already assigned to another User");
                return View(model);
            }

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Age = model.Age;
            user.Gendre = model.Gendre;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleName = role.Name,
                    isSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach(var role in model.Roles)
            {
                if(userRoles.Any(rolename => rolename == role.RoleName) && !role.isSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(rolename => rolename == role.RoleName) && role.isSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
