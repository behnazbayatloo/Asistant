using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.UserAgg
{
    public class CityRepository(ApplicationDbContext _dbcontext):ICityRepository
    {
      
        public async Task<PagedResult<CityDTO>> GetPagedCities(int pageNumber,int pageSize,CancellationToken ct)
        {
            var query = _dbcontext.Cities
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new CityDTO
                {
                    Id = c.Id,
                    CityName = c.Name
                   
                });

            return await query.ToPaginatedResult<CityDTO>(pageNumber, pageSize, ct);

        }
        public async Task<bool> DleteCity(int id,CancellationToken ct)
        {
            return await _dbcontext.Cities.Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(c => c.IsDeleted, true), ct) > 0;
        }
        public async Task<bool> CreateCity(CityDTO cityDTO, CancellationToken ct) 
        {
            var city = new City
            {
                Name = cityDTO.CityName
            };
            await _dbcontext.Cities.AddAsync(city,ct);
            return await _dbcontext.SaveChangesAsync(ct)>0;
        }
        public async Task<bool> ExistCity(string name ,CancellationToken ct)
        {
            return await _dbcontext.Cities.Where(c=>c.Name==name).AnyAsync(ct);
        }
    }
}
