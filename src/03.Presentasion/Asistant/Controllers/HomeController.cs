using Asistant.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Asistant.Controllers
{
    public class HomeController(ICategoryAppService _catapp,ILogger<HomeController> logger) : Controller
    {
      

        public async Task<IActionResult> Index(CancellationToken ct)
        {
         var categories=   await _catapp.GetAllCategories(ct);

            var categoryList = categories.Select(c => new CategoryHomeViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImagePath = c.ImagePath,
                HomeServices = c.HomeServices.Select(hs => new HomeServiceHomeViewModel
                {
                    Id=hs.Id,
                    Name=hs.Name,

                }).ToList()
            }).ToList();
            return View(categoryList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
