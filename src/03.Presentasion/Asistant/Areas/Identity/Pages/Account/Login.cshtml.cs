using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Identity.Pages.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
    [AllowAnonymous]
    public class LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<LoginModel> _logger) : PageModel
    {
      
        
       
            [BindProperty]
            public LoginViewModel Input { get; set; }
            [BindProperty]
            public string ReturnUrl { get; set; }
            public IActionResult OnGet(string returnUrl = null)
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return RedirectToPage("/Index", new { area = "" });
                }
                ReturnUrl = returnUrl ?? Url.Content("~/");
                return Page();
            }
            public async Task<IActionResult> OnPost()
            {

                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var user = await userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "اطلاعات ورود نامعتبر است.");
                    return Page();
                }
                var result = await signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("کاربر {UserId} وارد شد.", user.Id);
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToPage("/Index", new { area = "" }/* "Dashboard", new { area = "Admin" }*/);
                    }
                   if (roles.Contains("Customer"))
                    {
                        if (!string.IsNullOrEmpty(ReturnUrl))
                        {
                            return LocalRedirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToPage("/Index", new { area = "" } /*"Profil", new { area = "Customer" }*/);
                        }
                    }
                   if (roles.Contains("Expert"))
                {

                }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("کاربر {UserId} قفل شد.", user.Id);
                    return RedirectToPage("./Lockout");
                }
                ModelState.AddModelError(string.Empty, "ورود ناموفق بود.");
                return Page();
            }

            [Authorize]
            public async Task<IActionResult> OnPostLogout()
            {
                await signInManager.SignOutAsync();
                _logger.LogInformation("کاربر خارج شد.");
                return RedirectToAction("Index", "Home", new { area = "" });


            }
        }
    }

