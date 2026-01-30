using Asistant_Domain_Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Services
{
    public interface IAppUserService
    {
        Task<IdentityResult> ChangePassword(int userId, string password);
        Task<IdentityResult> DeleteAppUserById(int id, CancellationToken ct);
       
        Task<IdentityResult> UpdateAppUserFields(AppUserFieldsDTO updateFields);
    }
}
