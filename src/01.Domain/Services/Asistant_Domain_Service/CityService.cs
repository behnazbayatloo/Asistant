using Asistant_Domain_Core._commonEntities;
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
    public class CityService(ICityRepository _ctyrepo,ICityDapperRepository cityDapperRepository,ILogger<CityService>  logger):ICityService
    {
        public async Task<List<City>> GetAllCities (CancellationToken ct)=> await cityDapperRepository.GetAllCities(ct);
        public async Task<PagedResult<CityDTO>> GetPagedCities(int pageNumber,int pageSize,CancellationToken ct)
       => await _ctyrepo.GetPagedCities(pageNumber, pageSize, ct);
        public async Task<bool> DeleteCity(int id,CancellationToken ct)
            => await _ctyrepo.DleteCity(id, ct);
        public async Task<bool> CreateCity(CityDTO cityDTO,CancellationToken ct)
            => await _ctyrepo.CreateCity(cityDTO,ct);
        public async Task<bool> ExistCity(string name,CancellationToken ct)
            => await _ctyrepo.ExistCity(name, ct);
    }
}
