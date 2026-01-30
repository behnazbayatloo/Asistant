using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class AppUserAppService(UserManager<AppUser> _userManager,
        SignInManager<AppUser> _signInManager,
        RoleManager<IdentityRole<int>> _roleManager,
        IExpertService _expertsrv,
        ICustomerService _customersrv,
        ILogger<AppUserAppService> logger) : IAppUserAppService
    {
        public async Task<IdentityResult> Register(CancellationToken ct, RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                CreatedAt = DateTime.Now

            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                logger.LogInformation("کاربر جدید با ایمیل {Email} و نقش {Role} ساخته شد.",
                    user.Email, registerDTO.Role);


                if (registerDTO.Role == "Customer")
                {
                    var customerId= await _customersrv.CreateCustomer(user.Id, ct);
                    user.CustomerId = customerId;
                    await _userManager.UpdateAsync(user);
                    await _userManager.AddToRoleAsync(user, "Customer");

                }
                else if (registerDTO.Role == "Expert")
                {
                    var expertId= await _expertsrv.CreateExpert(user.Id, ct);
                    user.ExpertId = expertId;
                    await _userManager.UpdateAsync(user);
                    await _userManager.AddToRoleAsync(user, "Expert");

                }
                else
                {
                    return IdentityResult.Failed(new IdentityError
                    { Description = "نقش انتخاب‌شده معتبر نیست." });

                }
             
            }
            return result;
        }

        public async Task<LoginResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return new LoginResult
                {
                   Result = false,
                    Message="اطلاعات ورود نامعتبر است.",
                    Role=null
                };
              
            }
            var result = await _signInManager
                .PasswordSignInAsync(user, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                logger.LogInformation("کاربر {UserId} وارد شد.", user.Id);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    return new LoginResult
                    {
                        Result = result.Succeeded,
                        Message = "شما وارد شدید",
                        Role="Admin"

                    };
                }
                if (roles.Contains("Customer"))
                {
                    return new LoginResult
                    {
                        Result = result.Succeeded,
                        Message = "شما وارد شدید",
                        Role = "Customer"

                    };
                }
                if (roles.Contains("Expert"))
                {
                    return new LoginResult
                    {
                        Result = result.Succeeded,
                        Message = "شما وارد شدید",
                        Role = "Expert"

                    };
                }
            }
            if (result.RequiresTwoFactor)
            {
                return new LoginResult
                {
                    Result = result.Succeeded,
                    
                    RequiresTwoFactor= result.RequiresTwoFactor,
                    Message= "ورود نیاز به احراز هویت دومرحله‌ای دارد."

                };
            }
            if (result.IsLockedOut)
            {
                logger.LogWarning("کاربر {UserId} قفل شد.", user.Id);
                return new LoginResult
                {
                    Result = result.Succeeded,
                 IsLockedOut= result.IsLockedOut,
                    Message = "حساب کاربری شما قفل شده است."

                };
            }
            return new LoginResult
            {
                Result = result.Succeeded,
                Message = "ورود ناموفق بود."

            };
        }
    public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            logger.LogInformation("کاربر خارج شد.");
        }


    }
}