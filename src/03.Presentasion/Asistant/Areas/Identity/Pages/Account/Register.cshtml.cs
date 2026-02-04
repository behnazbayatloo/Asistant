using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Identity.Pages.Account
{
    public class InputModel
    {
        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string Email { get; set; }
        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = " یک نقش انتخاب کنید ")]
        public string Role { get; set; }
    }
    [Area("Identity")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class RegisterModel(IAppUserAppService appUserApp, ILogger<RegisterModel> logger) : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
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
      
        public async Task<IActionResult> OnPost(CancellationToken ct,string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new RegisterDTO
                {
                    Email=Input.Email,
                Password=Input.Password,
                Role=Input.Role

                };
                
                var result = await appUserApp.Register(ct,user);
                if (result.Succeeded)
                {
                    TempData["Succeed"] = "ثبت نام موفقیت آمیز بود";
                    return RedirectToPage("/Account/Login",new  { area ="Identity" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
           
           
            return Page();

        }

    }
}

