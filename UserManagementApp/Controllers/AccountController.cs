using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Models.Entities;
using UserManagementApp.Models.ViewModels;
using UserManagementApp.Services.Interfaces;

namespace UserManagementApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, IEmailService emailService, 
            SignInManager<AppUser> signInManager) 
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserToRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
                };

                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (createUserResult.Succeeded)
                {
                    // add role to newly created user
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "regular");
                    if(addRoleResult.Succeeded)
                    {
                        // send email confirmation link
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var link = Url.Action("ConfirmEmail", "Account", new { user.Email, token }, Request.Scheme);
                        var body = @$"Hi{user.FirstName},
Please click the link <a href='{link}'>here</a> to confirm your account's email";
                        await _emailService.SendEmailAsync(user.Email, "Confirm Email", body);

                        return RedirectToAction("RegisterCongrats", "Account", new {name=user.FirstName});
                    }
                    
                    foreach (var err in addRoleResult.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);

                    }
                }

                foreach(var err in createUserResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterCongrats(string name)
        {
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string Email, string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if(user != null)
            {
                var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, token);
                if(confirmEmailResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var err in confirmEmailResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(ModelState);
            }

            ModelState.AddModelError("", "Email confirmation failed");
            return View(ModelState);

        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserToLoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    if(await _userManager.IsEmailConfirmedAsync(user))
                    {
                        var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (loginResult.Succeeded)
                        {
                            if (string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return LocalRedirect(returnUrl);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invaid credentials");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email not confirmed yet!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invaid credentials");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("PasswordReset", "Account", new { user.Email, token }, Request.Scheme);
                    var body = @$"Hi{user.FirstName},
You appear to have forgotten your password, click the link <a href='{link}'>here</a> to reset your password";
                    await _emailService.SendEmailAsync(user.Email, "Forgot Password", body);

                    ViewBag.Message = "Reset password link has been sent to the email provided. If correct you should already get it by now.";
                    return View();
                }

                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult PasswordReset(string Email, string token)
        {
            var resetPasswordModel = new ResetPasswordViewModel { Email = Email, Token = token };
            return View(resetPasswordModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                   var resetPasswordResult =  await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (resetPasswordResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach(var err in  resetPasswordResult.Errors)
                        {
                            ModelState.AddModelError(err.Code, err.Description);
                        }
                        return View(model);
                    }

                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

// UserManager
// SignInManager
// RoleManager