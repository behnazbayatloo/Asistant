using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.Data
{
    public interface ICategoryRepository
    {
        Task<int> CreateCategory(string name, CancellationToken ct);
        Task<IEnumerable<GetCategoryDTO>> GetAllCtegories(CancellationToken ct);
        Task<GetCategoryDTO?> GetCategoryById(int id, CancellationToken ct);
        Task<bool> UpdateImage(int id, string imagePath, CancellationToken ct);
    }
}
