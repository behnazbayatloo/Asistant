using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.HomeServiceAgg
{
    public class HomeServiceRepository(ApplicationDbContext _dbcontext):IHomeServiceRepository
    {
        public async Task<int> CreateHomeService(InputHomeServiceDTO homeServiceDto,CancellationToken ct)
        {
            var homeService = new HomeService
            {
                Name = homeServiceDto.Name,
                BasePrice = homeServiceDto.BasePrice.Value,
                CategoryId = homeServiceDto.CategoryId.Value,
              
                Description = homeServiceDto.Description,
            };
            await _dbcontext.HomeServices.AddAsync(homeService,ct);
            await _dbcontext.SaveChangesAsync(ct);
            return homeService.Id;

        }

      
        public async Task<GetHomeServiceDTO?> GetHomeServiceById(int id, CancellationToken ct)
        {
            return await _dbcontext.HomeServices.AsNoTracking().Where(hs => hs.Id == id).Select(hs => new GetHomeServiceDTO
            {
                Id = hs.Id,
                Name = hs.Name,
                BasePrice = hs.BasePrice,
                CategoryId = hs.CategoryId,
                ImageId = hs.ImageId,
                Description = hs.Description,
                ImagePath=hs.Image.ImagePath,
                CategoryName = hs.Category.Name

            }).FirstOrDefaultAsync(ct);
        }
        public async Task<IEnumerable<GetHomeServiceDTO>>  GetHomeServicesByCategoryId(int categoryId, CancellationToken ct)
        {
            return await _dbcontext.HomeServices.AsNoTracking().Where(hs => hs.CategoryId == categoryId).Select(hs => new GetHomeServiceDTO
            {
                Id = hs.Id,
                Name = hs.Name,
                BasePrice = hs.BasePrice,
                CategoryId = hs.CategoryId,
                Description = hs.Description
                ,
                ImageId = hs.ImageId,
                ImagePath=hs.Image.ImagePath,
                CategoryName = hs.Category.Name
            }).ToListAsync(ct);
        }
        public async Task<bool> UpdateHomeService(InputHomeServiceDTO homeServiceDTO,CancellationToken ct)
        {
            return await _dbcontext.HomeServices.Where(hs => hs.Id == homeServiceDTO.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(hs => hs.Name, hs => homeServiceDTO.Name ?? hs.Name)
                .SetProperty(hs=> hs.BasePrice,hs=> homeServiceDTO.BasePrice ?? hs.BasePrice)
                .SetProperty(hs=>hs.Description,hs=> homeServiceDTO.Description ?? hs.Description)
                .SetProperty(hs=> hs.CategoryId,hs=> homeServiceDTO.CategoryId ?? hs.CategoryId)

               ,ct )>0;
        }
        
        public async Task<bool> UpdateImageId(int id,int imageId,CancellationToken ct)
        {
          return  await _dbcontext.HomeServices.Where(hs=>hs.Id==id)
                .ExecuteUpdateAsync(set=>set.SetProperty(hs=>hs.ImageId, imageId), ct)>0;
           
        }
      
        public async Task<PagedResult<GetHomeServiceDTO>> GetPagedHomeService(int pageNumber,int pageSize,CancellationToken ct)
        {
            var query = _dbcontext.HomeServices
                .AsNoTracking()
                .OrderBy(hs => hs.Id)
                .Select(hs => new GetHomeServiceDTO
                {
                    Id = hs.Id,
                   
                    Name = hs.Name,
                    BasePrice = hs.BasePrice,
                    CategoryId=hs.CategoryId,
                    Description= hs.Description,
                    ImageId=hs.ImageId,
                    ImagePath=hs.Image.ImagePath,
                    CategoryName = hs.Category.Name

                });

            return await query.ToPaginatedResult<GetHomeServiceDTO>(pageNumber, pageSize, ct);
        }
        public async Task<bool> DeleteHomeService(int id,CancellationToken ct)
        {
            return await _dbcontext.HomeServices.Where(hs => hs.Id == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(hs => hs.IsDeleted, true), ct) > 0;
        }
        public async Task<int?> GetHomeServiceImageId(int id, CancellationToken ct)
        {
            return await _dbcontext.HomeServices.Where(c => c.Id == id)
                .Select(c => c.ImageId).FirstOrDefaultAsync(ct);
        }

        public async Task<bool> DeleteHomeServicesByCategoryId(int categoryId,CancellationToken ct)
        {
          return  await _dbcontext.HomeServices.Where(hs => hs.CategoryId == categoryId)
                .ExecuteUpdateAsync(setter => setter.SetProperty(hs => hs.IsDeleted, true), ct) > 0;

        }
        public async Task<bool> ExistHomeService(string name, CancellationToken ct)
        {
            return await _dbcontext.HomeServices.AnyAsync(hs => hs.Name == name);
        }
        public async Task<List<HomeService>> GetHomeServicesById(List<int> list,CancellationToken ct)
        {
            return await _dbcontext.HomeServices.Where(hs=>list.Contains(hs.Id)).ToListAsync(ct);
        }
         public async Task<int?> GetCategoryId(int id, CancellationToken ct)
        {
            return await _dbcontext.HomeServices.Where(hs => hs.Id == id)
                .Select(hs => hs.CategoryId)
                .FirstOrDefaultAsync(ct);
        }
    }
}
