using Asistant_Domain_Core._commonEntities;
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
        Task<AdminDTO?> GetAdminById(int id, CancellationToken ct);
        Task<PagedResult<AdminDTO>> GetPagedAdmins(int pageNumber, int pageSize, CancellationToken ct);
        Task<IdentityResult> UpdateAppUserFields(AppUserFieldsDTO updateFields);
    }
}
