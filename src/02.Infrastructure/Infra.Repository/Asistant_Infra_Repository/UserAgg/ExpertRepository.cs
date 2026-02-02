using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.UserAgg
{
    public class ExpertRepository(ApplicationDbContext _dbcontext):IExpertRepository
    {
        public async Task<PagedResult<OutputExpertDTO>> GetAllExperts(int pageNumber, int pageSize,CancellationToken ct)
        {
          
            var query=  _dbcontext.Experts
                  .AsNoTracking()
                  .OrderBy(c => c.Id)
                 
                  .Select(e => new OutputExpertDTO

                  {
                      Id = e.Id,
                      UserId = e.UserId,
                      Email=e.User.Email,
                      FirstName= e.User.FirstName,
                      LastName= e.User.LastName,
                      CityName=e.City.Name,
                      Balance=e.User.Balance
                  });
         
            return await query.ToPaginatedResult<OutputExpertDTO>(pageNumber,pageSize,ct);
        }
        public async Task<bool> DeleteExpert (CancellationToken ct,int id)
        {
            return await _dbcontext.Experts
                .Where(e=>e.Id==id)
                .ExecuteUpdateAsync(e => e.SetProperty(e => e.IsDeleted, true), ct)>0;
        }
        public async Task<OutputExpertDTO?> GetExpertById(CancellationToken ct, int id)
        {
            return await _dbcontext.Experts.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new OutputExpertDTO
                {
                    Id = e.Id,
                    HomeServicesId = e.HomeServices.Select(hs=> hs.Id).ToList(),
                    CityId = e.CityId,
                    Email = e.User.Email,
                    FirstName = e.User.FirstName,
                    LastName = e.User.LastName,
                    UserId = e.UserId,
                    CityName= e.City.Name,
                    ImagePath= e.Image.ImagePath,
                    Balance=e.User.Balance
                    

                }).FirstOrDefaultAsync(ct);
        }
        public async Task<bool> UpdateExpert (CancellationToken ct,UpdateExpertDTO updateExpertDTO )
        {
            return await _dbcontext.Experts.Where(e => e.Id == updateExpertDTO.Id)
                .ExecuteUpdateAsync(setter =>
                setter
                .SetProperty(e => e.CityId, e => updateExpertDTO.CityId ?? e.CityId)
                .SetProperty(e => e.ImageId, e => updateExpertDTO.ImageId ?? e.ImageId)
                
                ,ct
                ) > 0;
        }
        public async Task<bool> UpdateHomeServicesForExpert(int expertId,List<HomeService> list, CancellationToken ct)
        {
            var expert = await _dbcontext.Experts.Where(e => e.Id == expertId).
                Include(e=>e.HomeServices)
                .FirstOrDefaultAsync(ct);
            if (expert == null) 
                return false;
            if(expert.HomeServices== null)
            {
                expert.HomeServices= new List<HomeService>();
            }
            expert.HomeServices.Clear();
            expert.HomeServices=list;
           return  await _dbcontext.SaveChangesAsync(ct)>0;
        }
     
        public async Task<int> CreateExpert(int userId,CancellationToken ct)
        {
            var expert = new Expert
            {
                UserId = userId
            };
            await _dbcontext.Experts.AddAsync(expert, ct);
            await _dbcontext.SaveChangesAsync(ct);
            return expert.Id;
        }
        public async Task<int> CreateExpertByAdmin(CreateExpertDTO expertDTO, CancellationToken ct)
        {
            var expert = new Expert
            {
                CityId = expertDTO.CityId,
                UserId = expertDTO.UserId,
                HomeServices = expertDTO.HomeServices ?? new List<HomeService>(),
            };

            await _dbcontext.Experts.AddAsync(expert, ct);
            await _dbcontext.SaveChangesAsync(ct);
            return expert.Id;
        }
        public async Task<bool> UpdateImageId(int id, int imageId, CancellationToken ct)
        {
            return await _dbcontext.Experts.Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(c => c.ImageId, imageId), ct) > 0;
        }
        public async Task<List<int>?> GetHomeServicesIdByExpertId(int expertId, CancellationToken ct)
        {
            return await _dbcontext.Experts
                .Where(e => e.Id == expertId)
                .SelectMany(e => e.HomeServices.Select(hs => hs.Id)
                ).ToListAsync(ct);
                
        }

        

    }
}
