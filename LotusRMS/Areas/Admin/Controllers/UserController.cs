using LotusRMS.Models;
using LotusRMS.Models.Service;
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
        private readonly UserManager<RMSUser> userManager;
        private readonly IUserService _iUserService;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserController(UserManager<RMSUser> userManager, IUserService iUserService,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _iUserService = iUserService;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users;
            var roles = roleManager.Roles.Where(x => x.Name != "SuperAdmin");
            var userList = new List<RMSUserVM>();
            foreach (var role in roles)
            {
                var userAtRole = userManager.GetUsersInRoleAsync(role.Name).Result.Select(u => new RMSUserVM()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Contact = u.Contact,
                    Email = u.Email,
                    Role = role.Name
                });
                userList.AddRange(userAtRole);
            }

            return View(userList);
        }
    }
}