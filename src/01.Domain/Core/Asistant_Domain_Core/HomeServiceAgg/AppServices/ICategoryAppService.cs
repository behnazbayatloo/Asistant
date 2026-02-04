using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.AppServices
{
    public interface ICategoryAppService
    {
        Task<Result<bool>> CreateCategory(InputCategoryDTO categoryDTO, CancellationToken ct);
        Task<bool> DeleteCategory(int id, CancellationToken ct);
        Task<IEnumerable<GetCategoryDTO>> GetAllCategories(CancellationToken ct);
        Task<GetCategoryDTO?> GetCategoryById(int id, CancellationToken ct);
        Task<PagedResult<GetCategoryDTO>> GetPagedCategories(int pageNumber, int pageSize, CancellationToken ct);
        Task<Result<bool>> UpdateCategory(InputCategoryDTO categoryDTO, CancellationToken ct);
    }
}
