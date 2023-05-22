using LotusRMS.Models;
using LotusRMS.Models.Dto.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using LotusRMS.Models.Viewmodels;
using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models.Viewmodels.User;
using DocumentFormat.OpenXml.Spreadsheet;
using LotusRMS.Utility;
using LotusRMS.Models.Service;

namespace LotusRMSweb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<RMSUser> _userManager;
        private readonly SignInManager<RMSUser> _signInManager;
        private readonly IUserStore<RMSUser> _userStore;
        private readonly IUserEmailStore<RMSUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly INotyfService _notyf;

        public string ReturnUrl { get; set; }
        public AccountController(UserManager<RMSUser> userManager,
            SignInManager<RMSUser> signInManager,
            IUserStore<RMSUser> userStore,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            INotyfService notyf)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _notyf = notyf;
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
                 /*   var message = new Message(new string[] { model.Email }, "Test email", "This is the content from our email.");
                   await _emailSender.SendEmailAsync(message);*/
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    _notyf.Success("User logged in successfully",5);
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
                    var user = new RMSUser(firstName: request.FirstName, middleName: request.MiddleName, lastName: request.LastName, contact: request.Contact)
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

                        await _emailSender.SendEmailAsync(new Message(new string[] { request.Email }, "Confirm your email" ,
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",null));

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

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");

            await _signInManager.SignOutAsync();
            _notyf.Success("Password changed successfully", 5);
            return RedirectToAction("Login","Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user)) {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                _logger.Log(LogLevel.Warning, passwordResetLink);
                var message = new Message(new string[] { model.Email }, "Forget password reset",
                    $"Reset the password by <a href='{HtmlEncoder.Default.Encode(passwordResetLink)}'> clicking here</a>.", null);
                await _emailSender.SendEmailAsync(message);

                return View("ForgetPasswordConfirmation");

            }

            return View("ForgetPasswordConfirmation");
        }
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]

        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            if(email==null || token == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
                return RedirectToAction("error");
            }
            var vm=new ResetPasswordVM() { Email=email, Token = token };
            return View(vm);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View(model);
            }
            return RedirectToAction("ResetPasswordConfirmation");

        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
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

        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var firstName = user.FirstName;
            var middleName = user.MiddleName;
            var contact = user.Contact;
            var lastName = user.LastName;
            var profilePicture = user.ProfilePicture;
            
            var model = new MyProfileVM()
            {
                Contact = contact,
                MiddleName = middleName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = profilePicture
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> MyProfile(UserRegistrationDto dto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }
            _notyf.Success("Profile Changed Successfully",5);
            return View(dto);
        }
        }
}
