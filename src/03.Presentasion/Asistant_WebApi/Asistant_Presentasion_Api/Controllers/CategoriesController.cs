using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asistant_Presentasion_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryAppService categoryApp): ControllerBase
    {
        [HttpGet] 
        public async Task<IActionResult> GetAllCategories(CancellationToken ct) 
        { 
            var categories = await categoryApp.GetAllCategories(ct);
            return Ok(categories);
        }
    }
}
