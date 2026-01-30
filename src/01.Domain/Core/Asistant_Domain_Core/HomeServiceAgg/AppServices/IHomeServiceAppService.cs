using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.AppServices
{
    public interface IHomeServiceAppService
    {
        Task<Result<bool>> CreateHomeService(InputHomeServiceDTO homeServiceDTO, CancellationToken ct);
        Task<bool> DeleteHomeService(int id, CancellationToken ct);
        Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct);
        Task<IEnumerable<GetHomeServiceDTO>> GetHomeServiceByCategoryId(int categoryId, CancellationToken ct);
        Task<GetHomeServiceDTO?> GetHomeServiceById(int id, CancellationToken ct);
        Task<PagedResult<GetHomeServiceDTO>> GetPagedHomeServices(int pageNumber, int pageSize, CancellationToken ct);
        Task<Result<bool>> UpdateHomeService(InputHomeServiceDTO homeServiceDTO, CancellationToken ct);
    }
}
