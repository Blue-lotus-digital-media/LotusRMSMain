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
using DocumentFormat.OpenXml.EMMA;
using LotusRMSweb.Areas.Identity.Pages.Account.Manage;
using LotusRMS.Models.Viewmodels.Login;
using Org.BouncyCastle.Crypto;
using AutoMapper;
using static Dapper.SqlMapper;

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
        private readonly IMapper _mapper;


        public AccountController(UserManager<RMSUser> userManager,
            SignInManager<RMSUser> signInManager,
            IUserStore<RMSUser> userStore,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            INotyfService notyf,
            IMapper mapper)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _notyf = notyf;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");
            UserLoginDto model = new UserLoginDto()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("message", "Email not registered yet");
                    _notyf.Error("Email not registered yet.", 10);
                    return View(model);
                }
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet. Check your email or ask admin to resend it.");
                    _notyf.Error("Email not confirmed yet.", 10);
                    return View(model);
                }
                if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    /*   var message = new Message(new string[] { model.Email }, "Test email", "This is the content from our email.");
                      await _emailSender.SendEmailAsync(message);*/
                    ModelState.AddModelError("message", "Invalid credentials");
                    _notyf.Error("Invalid password!!!", 10);
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    _notyf.Success("User logged in successfully", 5);
                    return LocalRedirect(model.ReturnUrl);
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    _notyf.Error("Error while attempting to login!!!, Try again after some time", 5);
                    return View(model);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var vm = new ResetPasswordVM()
            {
                Email = user.Email,
                Token = token
            };
            return View(vm);
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ResetPasswordVM vm)
        {
            if (vm.Email == null || vm.Token == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                return NotFound($"Unable to load user with Emial '{vm.Email}'.");
            }

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(vm.Token));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordSet = await _userManager.ResetPasswordAsync(user, token, vm.Password);
                if (passwordSet.Succeeded)
                {
                    ViewBag.StatusMessage = "Thank you for confirming your email.";
                    return RedirectToAction(nameof(ConfirmedEmail));
                }
                else
                {
                    user.EmailConfirmed = false;
                    await _userManager.UpdateAsync(user);
                    ViewBag.ErrorMessage = "Error While setting password.";
                    _notyf.Error("Error while setting password!!!", 5);
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Error confirming your email.";
                _notyf.Error("Error confirming your email.", 5);


            }
            return View(vm);
        }
        public IActionResult ConfirmedEmail()
        {
            return View(ViewBag.StatusMessage);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Register(string returnUrl)
        {
            ViewData["roles"] = _roleManager.Roles.Where(x => x.NormalizedName != "SuperAdmin").ToList();
            UserRegistrationDto model = new UserRegistrationDto();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
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
                    var result = await _userManager.CreateAsync(user);
                    var role = _roleManager.FindByIdAsync(request.UserRole).Result;


                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role.ToString());
                        //_logger.LogInformation("User created a new account with password.");

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { area = "", userId = user.Id, token = code }, Request.Scheme);
                        await _emailSender.SendEmailAsync(new Message(new string[] { request.Email }, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.", null));
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
            /*_logger.LogInformation("User changed their password successfully.");*/
            await _signInManager.SignOutAsync();
            _notyf.Success("Password changed successfully", 5);
            return RedirectToAction("Login", "Account");
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
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                _logger.Log(LogLevel.Warning, passwordResetLink);
                var message = new Message(new string[] { model.Email }, "Forget password reset",
                    $"Reset the password by <a href='{HtmlEncoder.Default.Encode(passwordResetLink)}'> clicking here</a>.", null);
                await _emailSender.SendEmailAsync(message);
                _notyf.Success("A password reset link was send to your email.", 5);
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
            if (email == null || token == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
                _notyf.Error("Invalid token!", 5);
                return RedirectToAction("error");
            }
            var vm = new ResetPasswordVM() { Email = email, Token = token };
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
                    var message = new Message(new string[] { model.Email }, "Forget password reset",
                   $"Welcome back. Your password has been reset. Now you can login to the app. <a href='{HtmlEncoder.Default.Encode(Url.Action(action: "Login", controller: "Account", values: "", protocol: Request.Scheme))}'> login</a>", null);
                    await _emailSender.SendEmailAsync(message);
                    _notyf.Success("Password reset success.", 5);
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            return RedirectToAction("ResetPasswordConfirmation");

        }
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [Authorize]
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

        [Authorize]
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
        [Authorize]
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
            _notyf.Success("Profile Changed Successfully", 5);
            var model = new MyProfileVM()
            {
                Contact = user.Contact,
                MiddleName = user.MiddleName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            UserLoginDto model = new UserLoginDto()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider :{remoteError} ");
                return View(nameof(Login), model);
            };
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Error loading external login information. ");
                return View(nameof(Login), model);
            }
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new RMSUser()
                        {
                            UserName = email,
                            Email = email
                        };
                        string FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? info.Principal.FindFirstValue(ClaimTypes.Name);
                        string LastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                        user.Update(FirstName, "", LastName, "0000000000");
                        await _userManager.CreateAsync(user);
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    // Let’s add the image to the User Claims 
                    if (info.Principal.HasClaim(c => c.Type == "image"))
                    {
                        await _userManager.AddClaimAsync(user, info.Principal.FindFirst("image"));
                    }
                    return LocalRedirect(returnUrl);
                }
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = $"Please contact on support.";
                return View("Error");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
