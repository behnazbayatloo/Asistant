using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class AppUserService(UserManager<AppUser> _userManager,ILogger<AppUserService> logger):IAppUserService
    {
        public async Task<IdentityResult> UpdateAppUserFields(AppUserFieldsDTO updateFields)
        {
            var user = await _userManager.FindByIdAsync(updateFields.Id.ToString());
            user.FirstName = updateFields.FirstName ?? user.FirstName;
            user.LastName = updateFields.LastName ?? user.LastName;
            user.Email = updateFields.Email ?? user.Email;
            return await _userManager.UpdateAsync(user);

           
        }
        public async Task<IdentityResult> ChangePassword(int userId,string password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.RemovePasswordAsync(user);
            return await _userManager.AddPasswordAsync(user, password);
        }
     

        public async Task<IdentityResult> DeleteAppUserById(int id, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.IsDeleted = true;
            return await _userManager.UpdateAsync(user);

        }
      
        }
}
