using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.Data;
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
    public class AppUserService(UserManager<AppUser> _userManager,IAppUserRepository _userRepository ,ILogger<AppUserService> logger) : IAppUserService
    {
        public async Task<IdentityResult> UpdateAppUserFields(AppUserFieldsDTO updateFields)
        {
            var user = await _userManager.FindByIdAsync(updateFields.Id.ToString());
            user.FirstName = updateFields.FirstName ?? user.FirstName;
            user.LastName = updateFields.LastName ?? user.LastName;
            user.Email = updateFields.Email ?? user.Email;
            user.Balance = updateFields.Ballance;
            return await _userManager.UpdateAsync(user);


        }
        public async Task<IdentityResult> ChangePassword(int userId, string password)
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
        public async Task<PagedResult<AdminDTO>> GetPagedAdmins(int pageNumber,int pageSize,CancellationToken ct)
            =>await _userRepository.GetAdminsPagedResults(pageNumber, pageSize, ct);
      public async Task<AdminDTO?> GetAdminById(int id, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user is not null)
            {
                var admin = new AdminDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Balance = user.Balance
                };
                return admin;

            }
            return null;
           
        }
       
       
        }

}
