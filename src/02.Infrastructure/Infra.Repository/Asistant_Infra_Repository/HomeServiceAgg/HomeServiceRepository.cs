using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Infra_Db_Sql.DbContext;
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
                BasePrice = homeServiceDto.BasePrice,
                CategoryId = homeServiceDto.CategoryId,
                ImageId = homeServiceDto.ImageId,
                Description = homeServiceDto.Description,
            };
            await _dbcontext.HomeServices.AddAsync(homeService);
            await _dbcontext.SaveChangesAsync(ct);
            return homeService.Id;

        }

        public async Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct)
        {
          return  await _dbcontext.HomeServices.AsNoTracking().Select(hs => new GetHomeServiceDTO
            {
                Id = hs.Id
                ,
                Name = hs.Name
                ,
                BasePrice = hs.BasePrice,
                CategoryId = hs.CategoryId,
                ImageId = hs.ImageId,
                Description = hs.Description
            }).ToListAsync(ct);
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
                Description = hs.Description

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
                ImageId = hs.ImageId
            }).ToListAsync(ct);
        }
        public async Task<bool> UpdateBasePrice(int id,decimal newPrice,CancellationToken ct)
        {
            await _dbcontext.HomeServices.Where(hs => hs.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(hs => hs.BasePrice, newPrice), ct);
            return true;
        }
        public async Task<bool> UpdateImagePath(int id,string imagePath,CancellationToken ct)
        {
            await _dbcontext.HomeServices.Where(hs=>hs.Id==id)
                .ExecuteUpdateAsync(set=>set.SetProperty(hs=>hs.Image.ImagePath, imagePath), ct);
            return true;
        }
        
    }
}
