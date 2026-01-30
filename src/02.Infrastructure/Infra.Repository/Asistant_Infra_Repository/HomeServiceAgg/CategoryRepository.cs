using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.UserAgg.DTOs;
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
    public class CategoryRepository(ApplicationDbContext _dbcontext):ICategoryRepository
    {
        public async Task<int> CreateCategory(string name,CancellationToken ct)
        {
            var category = new Category
            {
                Name = name,
            };
            await _dbcontext.Categories.AddAsync(category,ct);
            await _dbcontext.SaveChangesAsync(ct);
            return category.Id;
        }

        public async Task<IEnumerable<GetCategoryDTO>> GetAllCtegories (CancellationToken ct)
        {
           return  await _dbcontext.Categories.AsNoTracking().Select(
                c => new GetCategoryDTO
                {
                    Id=c.Id,
                    Name = c.Name,
                    ImagePath=c.Image.ImagePath,
                    HomeServices=c.HomeServices.Select(hs=> new HomeServiceDTO
                    {
                        Id=hs.Id,
                        Name=hs.Name,
                    }).ToList()

                }).ToListAsync(ct);

        }
        public async Task<GetCategoryDTO?> GetCategoryById(int id, CancellationToken ct)
        {
            return await _dbcontext.Categories.AsNoTracking().Where(c => c.Id == id).Select(c => new GetCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ImagePath = c.Image.ImagePath,
                HomeServices = c.HomeServices.Select(hs => new HomeServiceDTO { Id = hs.Id, Name = hs.Name }).ToList()
            }).FirstOrDefaultAsync(ct);
        }
        public async Task<bool> UpdateCategoryName(int id,string name,CancellationToken ct)
        {
            return await _dbcontext.Categories.Where(c => c.Id == id)
              .ExecuteUpdateAsync(set => set.SetProperty(c => c.Name,c=> name ?? c.Name), ct) > 0;
        }
        public async Task<bool> ExistCategoryName(string name,CancellationToken ct)
        {
            return await _dbcontext.Categories.AnyAsync(c=>c.Name==name, ct);
        }
        public async Task<bool> UpdateImageId(int id,int imageId, CancellationToken ct)
        {
            return await _dbcontext.Categories.Where(c => c.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(c => c.ImageId, imageId), ct)>0;
         
        }
        public async Task<PagedResult<GetCategoryDTO>> GetPagedCategory(int pageNumber,int pageSize,CancellationToken ct)
        {
            var query = _dbcontext.Categories
                 .AsNoTracking()
                 .OrderBy(c => c.Id)
                 .Select(c => new GetCategoryDTO
                 {
                     Id = c.Id,
                    ImagePath=c.Image.ImagePath,
                    Name = c.Name

                 });

            return await query.ToPaginatedResult<GetCategoryDTO>(pageNumber, pageSize, ct);
        }
        public async Task<bool> DeleteCategory(int id, CancellationToken ct)
        {
            return await _dbcontext.Categories.Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(c => c.IsDeleted, true), ct) > 0;
        }
        public async Task<int?> GetCategoryImageId(int id,CancellationToken ct)
        {
            return await _dbcontext.Categories.Where(c => c.Id == id)
                .Select(c => c.ImageId).FirstOrDefaultAsync(ct);
        }
    }
}
