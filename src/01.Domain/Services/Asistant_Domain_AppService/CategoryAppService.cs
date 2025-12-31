using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CategoryAppService(ICategoryService _catserv,ILogger<CategoryAppService> logger):ICategoryAppService
    {
        public async Task<IEnumerable<GetCategoryDTO>> GetAllCategories(CancellationToken ct) => await _catserv.GetAllCategories(ct);
    }
}
