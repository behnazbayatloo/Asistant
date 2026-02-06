using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asistant_Presentasion_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeServicesController(IHomeServiceAppService homeApp) : ControllerBase
    {
       
        [HttpGet]
        public async Task<IActionResult> GetAllHomeServices(CancellationToken ct) 
        { 
            var services = await homeApp.GetAllHomeServices(ct); 
            return Ok(services);
        }
    }
}
