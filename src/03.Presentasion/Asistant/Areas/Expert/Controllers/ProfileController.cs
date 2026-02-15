using Asistant.Areas.Customer.Models;
using Asistant.Areas.Expert.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpertViewModel = Asistant.Areas.Expert.Models.ExpertViewModel;

namespace Asistant.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "Expert")]
    public class ProfileController(ILogger<ProfileController> logger, UserManager<AppUser> _userManager,
        IExpertAppService _expertApp,ICityAppService cityApp, IHomeServiceAppService _hmapp) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {

            var userId = Int32.Parse(_userManager.GetUserId(User));
         
            var model = new ExpertViewModel();
            if (userId > 0)
            {
                var expert = await _expertApp.GetExpertByUserId(ct, userId);
                model = new ExpertViewModel
                {
                    Id = expert.Id,
                    UserId = userId,
                   
                    CityId = expert.CityId,
                    CityName = expert.CityName,
                    Email = expert.Email,
                    FirstName = expert.FirstName,
                    ImagePath = expert.ImagePath
                    ,
                    LastName = expert.LastName,
                    Ballance=expert.Balance,
                    HomeServiceNames=expert.HomeServicesNames

                };
            }

            return View(model);
        }
        public async Task<IActionResult> EditProfile(CancellationToken ct)
        {
            var userId = Int32.Parse(_userManager.GetUserId(User));
            var cities = (await cityApp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                     ,
                Text = c.Name
            }).ToList();
            cities.Insert(0, new SelectListItem { Value = "", Text = "یک شهر انتخاب کنید" });
            var expert = await _expertApp.GetExpertByUserId(ct, userId);
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
                CityId = expert.CityId == null ? null : expert.CityId.Value,
                Cities = cities,
                HomeServices = homeServices
                ,
                HomeServicesIds = expert.HomeServicesId ?? new List<int>(),
                ImagePath = expert.ImagePath,
                Ballance = expert.Balance


            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditExpertViewModel model, CancellationToken ct)
        {

            var cities = (await cityApp.GetAllCities(ct)).Select(c => new SelectListItem
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
                HomeServicesId = model.HomeServicesIds,
                Balance = model.Ballance

            };
            var result = await _expertApp.UpdateExpert(ct, editExpert);
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
        [HttpPost]
        public async Task<IActionResult> DeleteProfile(int id, CancellationToken ct)
        {
            var userId = Int32.Parse(_userManager.GetUserId(User));
            var result = await _expertApp.DeleteExpert(ct, id, userId);
            if (result)

            {
                return RedirectToAction("Index", new { area = "" });
            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";

            }
            return RedirectToAction("Index");

        }
    }
}
