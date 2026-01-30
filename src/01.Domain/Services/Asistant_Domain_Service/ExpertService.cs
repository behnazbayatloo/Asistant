using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class ExpertService(IExpertRepository _exprepo,ILogger<ExpertService> logger):IExpertService
    {
        public async Task<PagedResult<OutputExpertDTO>> GetPagedExperts(int pageNumber, int pageSize,CancellationToken ct)
        {
            if (pageSize <= 0) pageSize = 10;
            return await _exprepo.GetAllExperts(pageNumber, pageSize, ct);
        }
        public async Task<bool> DeleteExpert(CancellationToken ct, int id)
            => await _exprepo.DeleteExpert(ct, id);

        public async Task<OutputExpertDTO?> GetExpertById(CancellationToken ct, int id)
            => await _exprepo.GetExpertById(ct, id);
        public async Task<bool> UpdateExpert(CancellationToken ct, UpdateExpertDTO updateExpert)
             => await _exprepo.UpdateExpert(ct, updateExpert);
        public async Task<int> CreateExpert(int userId, CancellationToken ct) =>
            await _exprepo.CreateExpert(userId, ct);
        public async Task<int> CreateExpertByAdmin(CreateExpertDTO expertDTO,CancellationToken ct)
            => await _exprepo.CreateExpertByAdmin(expertDTO, ct);
        public async Task<bool> UpdateImageId(int id, int imageId,CancellationToken ct)
            => await _exprepo.UpdateImageId(id, imageId, ct);
        public async Task<List<int>?> GetHomeServiceIdByExpertId(int expertId, CancellationToken ct)
            => await _exprepo.GetHomeServicesIdByExpertId(expertId, ct);
        public async Task<bool> UpdateHomeServicesForExpert(int expertId, List<HomeService> list, CancellationToken ct)
            => await _exprepo.UpdateHomeServicesForExpert(expertId, list, ct);  
      
    }
}
