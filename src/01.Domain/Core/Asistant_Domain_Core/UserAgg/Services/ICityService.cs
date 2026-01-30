using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Services
{
    public interface ICityService
    {
        Task<bool> CreateCity(CityDTO cityDTO, CancellationToken ct);
        Task<bool> DeleteCity(int id, CancellationToken ct);
        Task<bool> ExistCity(string name, CancellationToken ct);
        Task<List<City>> GetAllCities(CancellationToken ct);
        Task<PagedResult<CityDTO>> GetPagedCities(int pageNumber, int pageSize, CancellationToken ct);
    }
}
