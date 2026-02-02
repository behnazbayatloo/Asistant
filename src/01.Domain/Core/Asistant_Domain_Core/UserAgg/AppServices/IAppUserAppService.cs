using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.AppServices
{
    public interface IAppUserAppService
    {
        Task<IdentityResult> CreateAdmin(CancellationToken ct, CreateAdminDTO admin);
        Task<bool> DeleteAdmin(CancellationToken ct, int id);
        Task<AdminDTO?> GetAdminById(int id, CancellationToken ct);
        Task<PagedResult<AdminDTO>> GetAdminsPagedResult(int pageNumber, int pageSize, CancellationToken ct);
        Task<LoginResult> Login(LoginDTO loginDTO);
        Task Logout();
        Task<IdentityResult> Register(CancellationToken ct, RegisterDTO registerDTO);
        Task<bool> UpdateAdmin(AppUserFieldsDTO adminDTO);
    }
}
