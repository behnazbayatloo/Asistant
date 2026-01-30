using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class CategoryService(ICategoryRepository _catrepo,ILogger<CategoryService> logger):ICategoryService
    {
        public async Task<IEnumerable<GetCategoryDTO>> GetAllCategories(CancellationToken ct) => await _catrepo.GetAllCtegories(ct);
        public async Task<PagedResult<GetCategoryDTO>> GetPagedCategories( int pageNumber,int pageSize,CancellationToken ct)
            => await _catrepo.GetPagedCategory(pageNumber, pageSize, ct);
        public async Task<bool> DeleteCategory(int id,CancellationToken ct)
            => await _catrepo.DeleteCategory(id, ct);
        public async Task<int?> GetCategoryImageId(int id,CancellationToken ct)
            => await _catrepo.GetCategoryImageId(id, ct);
        public async Task<int> CreateCategory(string name,CancellationToken ct)
            => await _catrepo.CreateCategory(name, ct);
        public async Task<bool> UpdateImageId(int id,int imageId,CancellationToken ct)
            => await _catrepo.UpdateImageId(id, imageId, ct);
        public async Task<bool> UpdateCategoryName(int id, string name, CancellationToken ct)
       => await _catrepo.UpdateCategoryName(id, name, ct);
        public async Task<bool> ExistCategoryName(string name, CancellationToken ct)
            => await _catrepo.ExistCategoryName(name, ct);
        public async Task<GetCategoryDTO?> GetCategoryById(int id,CancellationToken ct)
        => await _catrepo.GetCategoryById(id, ct);

   
    }
}
