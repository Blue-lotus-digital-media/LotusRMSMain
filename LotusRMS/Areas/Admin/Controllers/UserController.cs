using DocumentFormat.OpenXml.Spreadsheet;
using LotusRMS.Models;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Roles;
using LotusRMS.Models.Viewmodels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text;
using LotusRMS.Utility;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<RMSUser> _userManager;
        private readonly IUserService _iUserService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly INotyfService _notyf;
        public UserController(UserManager<RMSUser> userManager, IUserService iUserService,
                RoleManager<IdentityRole> roleManager, IEmailSender emailSender, INotyfService notyf)
        {
            _userManager = userManager;
            _iUserService = iUserService;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _notyf = notyf;
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

        public async Task<IActionResult> InviteAgain(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.EmailConfirmed = false;
            user.PasswordHash = "";
            await _userManager.UpdateAsync(user);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new {area="", userId = user.Id, token = code }, Request.Scheme);
             await _emailSender.SendEmailAsync(new Message(new string[] { user.Email }, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", null));
            _notyf.Success("An email with invite link was send to user email",5);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ManageRole(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            var roles = _roleManager.Roles;
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesVM>();
            foreach (var role in roles)
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
                if (await _userManager.IsInRoleAsync(user, role.Name).ConfigureAwait(true))
                {
                    userRolesViewModel.Selected = true;
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