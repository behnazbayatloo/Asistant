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
    public interface IExpertAppService
    {
        Task<IdentityResult> CreateExpertByAdmin(CreateExpertDTO expertDTO, CancellationToken ct);
        Task<bool> DeleteExpert(CancellationToken ct, int id, int userId);
        Task<OutputExpertDTO?> GetExpertById(CancellationToken ct, int id);
        Task<PagedResult<OutputExpertDTO>> GetPagedExperts(int pageNumber, int pageSize, CancellationToken ct);
        Task<bool> UpdateExpert(CancellationToken ct, UpdateExpertDTO updateExpert);
    }
}
