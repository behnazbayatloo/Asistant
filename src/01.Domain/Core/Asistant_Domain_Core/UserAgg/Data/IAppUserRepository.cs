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
        Task<decimal?> GetBallanceByCustomerId(int customerId, CancellationToken ct);
        Task<decimal?> GetBallanceByExpertId(int expertId, CancellationToken ct);
        Task<bool> UpdateBallanceForCustomer(int customerId, decimal ballance, CancellationToken ct);
        Task<bool> UpdateBallanceForExpert(int expertId, decimal ballance, CancellationToken ct);
    }
}
