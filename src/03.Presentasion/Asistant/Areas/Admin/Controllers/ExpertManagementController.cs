using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExpertManagementController (ILogger<ExpertManagementController> logger, IExpertAppService _expapp,
        ICityAppService _ctapp
        , IHomeServiceAppService _hmapp) : Controller
    {
        
        public async Task<IActionResult> ShowExperts(CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var result = await _expapp.GetPagedExperts(pageNumber, pageSize, ct);
            var pagedResult = new PagedViewModel<ExpertOutputViewModel>();
            pagedResult.Items = result.Items.Select(e => new ExpertOutputViewModel
            {
                CityId = e.CityId,
                Email = e.Email,
                FirstName = e.FirstName,

                Id = e.Id,
                LastName = e.LastName,
                UserId = e.UserId,
                CityName=e.CityName
            }).ToList();
            pagedResult.PageNumber = pageNumber;
            pagedResult.PageSize = pageSize;
            pagedResult.TotalCount = result.TotalCount;
            pagedResult.TotalPages = result.TotalPages;
            return View(pagedResult);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteExpert(CancellationToken ct, int id, int userId)
        {
            var result = await _expapp.DeleteExpert(ct, id, userId);
            if (result)

            {
                TempData["Succeed"] = "کارشناس با موفقیت حذف شد";
                return RedirectToAction("ShowExperts");
            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";
                return RedirectToAction("ShowExperts");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditExpert(int id, int userId, CancellationToken ct)
        {
            var cities =( await _ctapp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            cities.Insert(0, new SelectListItem { Value = "", Text = "یک شهر انتخاب کنید" }); 
            var expert = await _expapp.GetExpertById(ct, id);
            var homeServices = (await _hmapp.GetAllHomeServices(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            homeServices.Insert(0, new SelectListItem { Value = "", Text = "یک سرویس انتخاب کنید" });
            

            var result = new EditExpertViewModel
            {

                Email = expert.Email,
                FirstName = expert.FirstName,
                LastName = expert.LastName,
                Id = expert.Id,
                UserId = expert.UserId,
                CityId = expert.CityId==null ? null : expert.CityId.Value,
                Cities =cities,
                HomeServices = homeServices
                ,
                HomeServicesIds = expert.HomeServicesId ?? new List<int>() ,
                ImagePath = expert.ImagePath


            }; 
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditExpert(EditExpertViewModel model, CancellationToken ct)
        {

            var cities = (await _ctapp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            cities.Insert(0, new SelectListItem { Value = "", Text = "یک شهر انتخاب کنید" });
            var homeServices = (await _hmapp.GetAllHomeServices(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                  ,
                Text = c.Name
            }).ToList();
            homeServices.Insert(0, new SelectListItem { Value = "", Text = "یک سرویس انتخاب کنید" });
            if (!ModelState.IsValid)
            {

                model.Cities = cities;
                model.HomeServices = homeServices;
                return View(model);
            }
            model.Cities = cities;
            model.HomeServices = homeServices;
            var editExpert = new UpdateExpertDTO
            {
                Id = model.Id,

                CityId = model.CityId,
                Email = model.Email
                ,
                FirstName = model.FirstName
                ,
                LastName = model.LastName,
                Password = model.Password,
                UserId = model.UserId,
                Image = model.Image,
                HomeServicesId= model.HomeServicesIds

            };
            var result = await _expapp.UpdateExpert(ct, editExpert);
            if (result)
            {
                ViewBag.Succeed = "تغییرات با موفقیت ذخیره شد";

              
            }
            else
            {
                ViewBag.Error = "تغییرات ثبت نشد";

                ModelState.AddModelError("", "ویرایش ناموفق بود");
            }

            return View(model);


        }

        public async Task<IActionResult> CreateExpert(CancellationToken ct)
        {
            var cities = await _ctapp.GetAllCities(ct);
            var homeServices = await _hmapp.GetAllHomeServices(ct);
            var model = new CreateExpertViewModel
            {
                Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString()
                ,
                    Text = c.Name
                }),
                HomeServices = homeServices.Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.Name
                })

            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpert(CreateExpertViewModel model, CancellationToken ct)
        {
            var cities = await _ctapp.GetAllCities(ct);
            var homeServices = await _hmapp.GetAllHomeServices(ct);
            if (!ModelState.IsValid)
            {

                model.Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString()
                    ,
                    Text = c.Name
                });
                model.HomeServices = homeServices.Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.Name
                });
                return View(model);
            }
            model.Cities = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                   ,
                Text = c.Name
            });
            model.HomeServices = homeServices.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            });
            var expert = new CreateExpertDTO
            {
                HomeServicesId = model.HomeServicesIds,
                Email = model.Email,
                CityId = model.CityId,
                FirstName = model.FirstName,
                Image = model.Image,
                LastName = model.LastName,
                Password = model.Password,

            };
            var result = await _expapp.CreateExpertByAdmin(expert, ct);
            if (result.Succeeded)
            {


                ViewBag.Succeed = "کارشناس جدید ثبت شد";
            }
            else
            {


                ViewBag.Error = "عملیات موفقیت آمیز نبود";
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
     
    }
}
