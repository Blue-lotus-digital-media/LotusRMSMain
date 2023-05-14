using LotusRMS.Models;
using LotusRMS.Models.Dto.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace LotusRMSweb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<RMSUser> _userManager;
        private readonly SignInManager<RMSUser> _signInManager;
        private readonly IUserStore<RMSUser> _userStore;
        private readonly IUserEmailStore<RMSUser> _emailStore;
        private readonly IEmailSender _emailSender;

        private readonly RoleManager<IdentityRole> _roleManager;
        public string ReturnUrl { get; set; }
        public AccountController(UserManager<RMSUser> userManager,
            SignInManager<RMSUser> signInManager,
            IUserStore<RMSUser> userStore,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore(); 
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
           
            returnUrl ??= Url.Content("~/");
            UserLoginDto model = new UserLoginDto();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return LocalRedirect(model.ReturnUrl);
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            ViewData["roles"] = _roleManager.Roles.Where(x => x.NormalizedName != "SuperAdmin").ToList();
            UserRegistrationDto model = new UserRegistrationDto();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await _userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new RMSUser(firstName:request.FirstName,middleName:request.MiddleName, lastName:request.LastName, contact:request.Contact)
                    {
     
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    await _userStore.SetUserNameAsync(user, request.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, request.Password);
                    var role = _roleManager.FindByIdAsync(request.UserRole).Result;


                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role.ToString());
                        //_logger.LogInformation("User created a new account with password.");

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = request.ReturnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(request.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = request.Email, returnUrl = request.ReturnUrl });
                        }
                        else
                        {
                            // await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(request.ReturnUrl);
                        }
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        private IUserEmailStore<RMSUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<RMSUser>)_userStore;
        }


    }
}
