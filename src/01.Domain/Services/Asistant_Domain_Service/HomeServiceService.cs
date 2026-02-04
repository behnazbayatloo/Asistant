using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class HomeServiceService(IHomeServiceRepository _hmrepo, ILogger<HomeServiceService> logger):IHomeServiceService
    {
      

        public async Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct) =>
            await _hmrepo.GetAllHomeServices(ct);
        public async Task<PagedResult<GetHomeServiceDTO>> GetPagedHomeService(int pageNumber,int pageSize,CancellationToken ct)
            => await _hmrepo.GetPagedHomeService(pageNumber, pageSize, ct);

        public async Task<int?> GetHomeServiceImageId(int id, CancellationToken ct)
             => await _hmrepo.GetHomeServiceImageId(id,ct);
        public async Task<bool> DeleteHomeServicesByCategoryId(int categoryId, CancellationToken ct)
            => await _hmrepo.DeleteHomeServicesByCategoryId(categoryId,ct);
        public async Task<bool> DeleteHomeService(int id,CancellationToken ct)
            => await _hmrepo.DeleteHomeService(id,ct);
        public async Task<bool> UpdateImageId(int id,int imageId,CancellationToken ct)
            => await _hmrepo.UpdateImageId(id,imageId,ct);
        public async Task<int> CreateHomService(InputHomeServiceDTO inputHomeServiceDTO ,CancellationToken ct)
            => await _hmrepo.CreateHomeService(inputHomeServiceDTO ,ct);
        public async Task<bool> UpdateHomeService(InputHomeServiceDTO homeServiceDTO, CancellationToken ct)
        {
            return await _hmrepo.UpdateHomeService(homeServiceDTO ,ct);
        }
        public async Task<IEnumerable<GetHomeServiceDTO>> GetHomeServicesByCategoryId(int categoryId, CancellationToken ct)
            => await _hmrepo.GetHomeServicesByCategoryId(categoryId ,ct);
        public async Task<bool> ExistHomeService (string name,CancellationToken ct)
            => await _hmrepo.ExistHomeService(name,ct);
        public async Task<GetHomeServiceDTO?> GetHomeServiceById(int id,CancellationToken ct)
        => await _hmrepo.GetHomeServiceById(id,ct);
        public async Task<List<HomeService>> GetHomeServicesById(List<int> list, CancellationToken ct)
            => await _hmrepo.GetHomeServicesById(list,ct);
    }
}
