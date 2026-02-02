using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Data
{
    public interface IAppUserRepository
    {
        Task<PagedResult<AdminDTO>> GetAdminsPagedResults(int pageNumber, int pageSize, CancellationToken ct);
    }
}
