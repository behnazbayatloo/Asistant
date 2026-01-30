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
        Task<LoginResult> Login(LoginDTO loginDTO);
        Task Logout();
        Task<IdentityResult> Register(CancellationToken ct, RegisterDTO registerDTO);
    }
}
