using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.Data
{
    public interface IHomeServiceRepository
    {
        Task<int> CreateHomeService(InputHomeServiceDTO homeServiceDto, CancellationToken c);
        Task<bool> DeleteHomeService(int id, CancellationToken ct);
        Task<bool> DeleteHomeServicesByCategoryId(int categoryId, CancellationToken ct);
        Task<bool> ExistHomeService(string name, CancellationToken ct);
        Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct);
        Task<int?> GetCategoryId(int id, CancellationToken ct);
        Task<GetHomeServiceDTO?> GetHomeServiceById(int id, CancellationToken ct);
        Task<int?> GetHomeServiceImageId(int id, CancellationToken ct);
        Task<IEnumerable<GetHomeServiceDTO>> GetHomeServicesByCategoryId(int categoryId, CancellationToken ct);
        Task<List<HomeService>> GetHomeServicesById(List<int> list, CancellationToken ct);
        Task<PagedResult<GetHomeServiceDTO>> GetPagedHomeService(int pageNumber, int pageSize, CancellationToken ct);
      
        Task<bool> UpdateHomeService(InputHomeServiceDTO homeServiceDTO, CancellationToken ct);
        Task<bool> UpdateImageId(int id, int imageId, CancellationToken ct);
    
    }
}
