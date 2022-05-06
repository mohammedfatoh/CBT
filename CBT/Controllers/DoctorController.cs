using CBT.Data;
using CBT.Models;
using CBT.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBT.Controllers
{
    public class DoctorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DoctorController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
             ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.Select(role => new RoleViewModel { RoleName = role.Name }).ToListAsync();

            var viewModel = new AddUserViewModel
            {
              Roles = roles
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            if(!model.Roles.Any(role => role.isSelected))
            {
                ModelState.AddModelError("Roles", "please select at least one role");
                return View(model);
            }

            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already exist");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName is already exist");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                Gendre = model.Gendre,
                Age = model.Age,
                IsEnabled = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //we add roles to user which is selected

                await _userManager.AddToRolesAsync(user, model.Roles.Where(role => role.isSelected).Select(role => role.RoleName));

                // create and Add Data To Doctor 
                Doctor doctor = new Doctor()
                {
                    Name = user.FullName,
                    Gendre = user.Gendre,
                    Age = user.Age,
                    UserId = user.Id
                };

                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }

                return RedirectToAction("Index", "Users");
        }
    }
}
