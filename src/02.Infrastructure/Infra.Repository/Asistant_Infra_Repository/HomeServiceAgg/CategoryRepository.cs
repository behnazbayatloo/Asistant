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
    public class CategoryRepository(ApplicationDbContext _dbcontext):ICategoryRepository
    {
        public async Task<int> CreateCategory(string name)
        {
            var category = new Category
            {
                Name = name,
            };
            await _dbcontext.Categories.AddAsync(category);
            await _dbcontext.SaveChangesAsync();
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

        public async Task<bool> UpdateImage(int id,string imagePath, CancellationToken ct)
        {
            return await _dbcontext.Categories.Where(c => c.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(c => c.Image.ImagePath, imagePath), ct)>0;
         
        }
    }
}
