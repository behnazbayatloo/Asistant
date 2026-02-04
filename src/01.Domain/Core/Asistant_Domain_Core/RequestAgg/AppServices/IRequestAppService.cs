using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.RequestAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.AppServices
{
    public interface IRequestAppService
    {
        Task<bool> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO);
        Task<bool> DeleteRequest(int id, CancellationToken ct);
        Task<PagedResult<OutputRequestDTO>> GetPagedRequest(int pageNumber, int pageSize, CancellationToken ct);
        Task<OutputRequestDTO?> GetRequestById(int id, CancellationToken ct);
        Task<bool> RejectRequest(int id, CancellationToken ct);
    }
}
