using Asistant.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace Asistant.ViewComponents
{
    public class ServicesMenuViewComponent (ICategoryAppService _catapp) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken ct = default)
        {
            var categories = await _catapp.GetAllCategories(ct);

            var categoryList = categories.Select(c => new CategoryHomeViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImagePath = c.ImagePath,
                HomeServices = c.HomeServices.Select(hs => new HomeServiceHomeViewModel
                {
                    Id = hs.Id,
                    Name = hs.Name,

                }).ToList()
            }).ToList();
            return View(categoryList);
        }
    }
}

