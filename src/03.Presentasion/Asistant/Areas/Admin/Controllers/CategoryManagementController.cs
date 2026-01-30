using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.UserAgg.DTOs;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryManagementController(ILogger<CategoryManagementController> logger,
        ICategoryAppService categoryApp) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct, int pageNumber = 1, int pageSize = 4)
        {
            var pagedCategory = await categoryApp.GetPagedCategories(pageNumber, pageSize, ct);
            var pagedResult = new PagedViewModel<CategoryViewModel>();
            pagedResult.Items = pagedCategory.Items.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ImagePath=c.ImagePath,

            }).ToList();
            pagedResult.PageNumber = pagedCategory.PageNumber;
            pagedResult.PageSize = pagedCategory.PageSize;
            pagedResult.TotalCount = pagedCategory.TotalCount;
            pagedResult.TotalPages = pagedCategory.TotalPages;
            return View(pagedResult);

            
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id,CancellationToken ct)
        {
            var result = await categoryApp.DeleteCategory(id, ct);
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
        public async Task<IActionResult> CreateCategory(CancellationToken ct)
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = new InputCategoryDTO
            {
                Image=model.Image,
                Name=model.Name
            };
            var result = await categoryApp.CreateCategory(category, ct);
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
        public async Task<IActionResult> EditCategory(int id, CancellationToken ct)
        {
           var category  = await categoryApp.GetCategoryById(id, ct);
            var model = new EditCategoryViewModel
            { 
            Id=category.Id,
            ImagePath=category.ImagePath,
            Name=category.Name};
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(EditCategoryViewModel model, CancellationToken ct)
        {
            if(!ModelState.IsValid)
            {
                return View(model); 
            }
            var category = new InputCategoryDTO
            {
                Image = model.Image,
                Name = model.Name,
                Id = model.Id
            };
            var result= await categoryApp.UpdateCategory(category, ct);
            var updatedCategory = await categoryApp.GetCategoryById(model.Id, ct);
            model.Id = updatedCategory.Id;
            model.ImagePath = updatedCategory.ImagePath;
            model.Name = updatedCategory.Name;
           
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
    }
}
