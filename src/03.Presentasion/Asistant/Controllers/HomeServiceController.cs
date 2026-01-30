using Asistant.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asistant.Controllers
{
    public class HomeServiceController (ILogger<HomeServiceController> logger,
        IHomeServiceAppService homeServiceApp): Controller
    {
        public async Task<IActionResult> Index(int id,CancellationToken ct)
        {
            var homeService = await homeServiceApp.GetHomeServiceById(id, ct);
            var model = new HomeServiceHomeViewModel
            {
                Id = homeService.Id,
                Name = homeService.Name,
                BasePrice=homeService.BasePrice,
                CategoryName = homeService.CategoryName,
                Description = homeService.Description,
                ImagePath = homeService.ImagePath
            };
            return View(model);
        }
    }
}
