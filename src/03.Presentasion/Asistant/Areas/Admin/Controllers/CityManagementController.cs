using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CityManagementController(ILogger<CityManagementController> logger,ICityAppService _cityapp) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct, int pageNumber = 1, int pageSize = 4)
        {
            var pagedCities = await _cityapp.GetPagedCities(pageNumber, pageSize, ct);
            var pagedResult = new PagedViewModel<CityViewModel>();
            pagedResult.Items = pagedCities.Items.Select (c=> new CityViewModel
            {
                Id=c.Id,
                Name=c.CityName
                
            }).ToList();
            pagedResult.PageNumber= pagedCities.PageNumber;
            pagedResult.PageSize= pagedCities.PageSize;
            pagedResult.TotalCount= pagedCities.TotalCount;
            pagedResult.TotalPages= pagedCities.TotalPages;
            return View(pagedResult);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCity(int id,CancellationToken ct)
        {
            var result = await _cityapp.DeleteCity(id, ct);
            if (result)

            {
                TempData["Succeed"] = "شهر با موفقیت حذف شد";

            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateCity( CancellationToken ct)
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCity(CityViewModel model ,CancellationToken ct)
        {
            if (!ModelState.IsValid) 
            { 
                return View(model);
            }
            var city = new CityDTO
            {
                CityName = model.Name
            };
            var result = await _cityapp.CreateCity(city, ct);
            if (result.IsSuccess)

            {
                TempData["Succeed"] = result.Message;

            }
            else
            {
                TempData["Error"] = result.Message;

            }
            
            return View(model);
        }

    }
}
