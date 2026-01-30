using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeServiceManagementController(IHomeServiceAppService homeServiceApp,
        ICategoryAppService categoryApp,
        ILogger<HomeServiceManagementController> logger) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct, int pageNumber = 1, int pageSize = 4)
        {
            var pagedHomeServices = await homeServiceApp.GetPagedHomeServices(pageNumber, pageSize, ct);
            var pagedResult = new PagedViewModel<HomeServiceViewModel>();
            pagedResult.Items = pagedHomeServices.Items.Select(c => new HomeServiceViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImagePath = c.ImagePath,
                BasePrice=c.BasePrice,
                CategoryId = c.CategoryId,
                Description = c.Description,
                CategoryName = c.CategoryName,

            }).ToList();
            pagedResult.PageNumber = pagedHomeServices.PageNumber;
            pagedResult.PageSize = pagedHomeServices.PageSize;
            pagedResult.TotalCount = pagedHomeServices.TotalCount;
            pagedResult.TotalPages = pagedHomeServices.TotalPages;
            return View(pagedResult);


        }
        [HttpPost]
        public async Task<IActionResult> DeleteHomeService(int id, CancellationToken ct)
        {
            var result = await homeServiceApp.DeleteHomeService(id, ct);
            if (result)

            {
                TempData["Succeed"] = "دسته بندی با موفقیت حذف شد";

            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateHomeService(CancellationToken ct)
        {
            var categories = await categoryApp.GetAllCategories(ct);
            var model = new HomeServiceViewModel();
            
            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                 ,
                Text = c.Name
            });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHomeService(HomeServiceViewModel model, CancellationToken ct)
        {
            var categories = await categoryApp.GetAllCategories(ct);
            if (!ModelState.IsValid)
            {
                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString()
                 ,
                    Text = c.Name
                });
                return View(model);
            }
            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                ,
                Text = c.Name
            });
            var homeService = new InputHomeServiceDTO
            {
                Name = model.Name,
                BasePrice=model.BasePrice,
                CategoryId = model.CategoryId,
                Description=model.Description,
                Image=model.Image
             
            };
            var result = await homeServiceApp.CreateHomeService(homeService, ct);
            if (result.IsSuccess)
            {

                ViewBag.Succeed = result.Message;
            }
            else
            {


                ViewBag.Error = result.Message;
            }
            return View(model);
        }
         
        public async Task<IActionResult> EditHomeService(int id,CancellationToken ct)
        {
            var categories = (await categoryApp.GetAllCategories(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                   ,
                Text = c.Name
            }).ToList();
            categories.Insert(0, new SelectListItem { Value = "", Text = "یک دسته بندی انتخاب کنید" });
            var homeService = await homeServiceApp.GetHomeServiceById(id, ct);


            var model = new EditHomeServiceViewModel
            {
                Id=homeService.Id,
                Name=homeService.Name,
                BasePrice=homeService.BasePrice,
                CategoryId = homeService.CategoryId,
                Description= homeService.Description,
                ImagePath=homeService.ImagePath,
                

            };
            model.Categories = categories;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditHomeService(EditHomeServiceViewModel model, CancellationToken ct)
        {
            var categories = (await categoryApp.GetAllCategories(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            categories.Insert(0, new SelectListItem { Value = "", Text = "یک دسته بندی انتخاب کنید" });

            if (!ModelState.IsValid)
            {
                model.Categories=categories;
                return View(model);

            }
            model.Categories = categories;
            var homeService = new InputHomeServiceDTO
            {
                BasePrice=model.BasePrice
                ,CategoryId=model.CategoryId,
                Description=model.Description
               ,Id=model.Id
              ,Image=model.Image,
                Name=model.Name
            };
            var result = await homeServiceApp.UpdateHomeService(homeService, ct);
            if(result.IsSuccess)
            {
                ViewBag.Succeed = result.Message;
            }
            else
            {
                ViewBag.Error = result.Message;

            }
            return View (model);
        }
    }
}
