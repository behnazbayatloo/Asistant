using Asistant.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asistant.Controllers
{
    public class HomeServicesController(ILogger<HomeServicesController> logger,
        IHomeServiceAppService homeServiceApp) : Controller
    {
        public async Task<IActionResult> Index(int categoryId ,CancellationToken ct)
        {
            var homeServices= await homeServiceApp.GetHomeServiceByCategoryId(categoryId, ct);
            var model = new HomeServicePage();
            model.List = homeServices.Select(hs => new HomeServiceHomeViewModel
            {
                Id = hs.Id,
                Name = hs.Name,
                Description=hs.Description.Length>70 
                ?hs.Description.Substring(0,70)+"..." :hs.Description,
                BasePrice = hs.BasePrice,
                CategoryName = hs.CategoryName,
                ImagePath=hs.ImagePath
            }).ToList();
            if (model.List.Any())
            {
                model.CategoryName = model.List[0].CategoryName; 
            } 
            else
            {
                model.CategoryName = ""; 
            }
            return View(model);
        }
    }
}
