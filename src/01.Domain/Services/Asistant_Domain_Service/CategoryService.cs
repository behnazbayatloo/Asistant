using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Microsoft.Extensions.Logging;
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
    }
}
