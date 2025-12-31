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
        Task<IEnumerable<GetCategoryDTO>> GetAllCategories(CancellationToken ct);
    }
}
