using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.UserAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Data
{
    public interface IExpertRepository
    {
        Task<int> CreateExpert(int userId, CancellationToken ct);
        Task<int> CreateExpertByAdmin(CreateExpertDTO expertDTO, CancellationToken ct);
        Task<bool> DeleteExpert(CancellationToken ct, int id);
        Task<PagedResult<OutputExpertDTO>> GetAllExperts(int pageNumber, int pageSize, CancellationToken ct);
        Task<OutputExpertDTO?> GetExpertById(CancellationToken ct, int id);
        Task<OutputExpertDTO?> GetExpertByUserId(CancellationToken ct, int userId);
        Task<List<int>?> GetHomeServicesIdByExpertId(int expertId, CancellationToken ct);
       
        Task<bool> UpdateExpert(CancellationToken ct, UpdateExpertDTO updateExpertDTO);
        Task<bool> UpdateHomeServicesForExpert(int expertId, List<HomeService> list, CancellationToken ct);
        Task<bool> UpdateImageId(int id, int imageId, CancellationToken ct);
    }
}
