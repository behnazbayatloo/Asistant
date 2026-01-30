using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.Services
{
    public interface ICategoryService
    {
        Task<int> CreateCategory(string name, CancellationToken ct);
        Task<bool> DeleteCategory(int id, CancellationToken ct);
        Task<bool> ExistCategoryName(string name, CancellationToken ct);
        Task<IEnumerable<GetCategoryDTO>> GetAllCategories(CancellationToken ct);
        Task<GetCategoryDTO?> GetCategoryById(int id, CancellationToken ct);
        Task<int?> GetCategoryImageId(int id, CancellationToken ct);
        Task<PagedResult<GetCategoryDTO>> GetPagedCategories(int pageNumber, int pageSize, CancellationToken ct);
        Task<bool> UpdateCategoryName(int id, string name, CancellationToken ct);
        Task<bool> UpdateImageId(int id, int imageId, CancellationToken ct);
    }
}
