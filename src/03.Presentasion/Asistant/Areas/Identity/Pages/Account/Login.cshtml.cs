using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
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
    public class LoginModel(IAppUserAppService appUserApp, ILogger<LoginModel> _logger) : PageModel
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
            var result = await appUserApp.Login(
                new LoginDTO { 
                    Email=Input.Email,
                    Password=Input.Password,
                    RememberMe=Input.RememberMe
                });

                if (result.Result && result.Role=="Admin")
                {
                return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
                
                }
            if (result.Result && result.Role == "Customer")
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });

                }
               
            }
            if (result.Result && result.Role == "Expert")
            {
                return RedirectToAction("Index", "Home", new { area = "" });

            }
           
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                   
                    return RedirectToPage("./Lockout");
                }
            if (!string.IsNullOrEmpty(result.Message))
            { 
                ModelState.AddModelError(string.Empty, result.Message);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "ورود ناموفق بود.");
            }
            return Page();
            }

            
            public async Task<IActionResult> OnPostLogout()
            {
                  await appUserApp.Logout();
                return RedirectToAction("Index", "Home", new { area = "" });


            }
        }
    }

