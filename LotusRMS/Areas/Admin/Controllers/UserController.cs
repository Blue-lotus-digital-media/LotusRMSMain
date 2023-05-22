using LotusRMS.Models;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Roles;
using LotusRMS.Models.Viewmodels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<RMSUser> _userManager;
        private readonly IUserService _iUserService;
        private readonly RoleManager<IdentityRole> _roleManager;
    public UserController(UserManager<RMSUser> userManager, IUserService iUserService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _iUserService = iUserService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        { 
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesVM>();
            foreach (RMSUser user in users)
            {
                if (_userManager.IsInRoleAsync(user, "SuperAdmin").Result)
                {
                    if (!User.IsInRole("SuperAdmin"))
                    {
                        continue;
                    }
                }
                    var thisViewModel = new UserRolesVM();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.Email = user.Email;
                    thisViewModel.FirstName = user.FirstName;
                    thisViewModel.MiddleName = user.MiddleName;
                    thisViewModel.LastName = user.LastName;
                    thisViewModel.Contact = user.Contact;

                    thisViewModel.Roles = await GetUserRoles(user);
                    userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(RMSUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }



        public async Task<IActionResult> ManageRole(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesVM>();
            foreach (var role in _roleManager.Roles)
            {
                if (role.Name == "SuperAdmin")
                {
                    if (!User.IsInRole("SuperAdmin"))
                    {
                        continue;
                    }
                }
                var userRolesViewModel = new ManageUserRolesVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageRole(List<ManageUserRolesVM> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}