using Asistant.Areas.Customer.Models;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_FrameWork.UIExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asistant.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class RequestController(ILogger<RequestController> logger,
        IRequestAppService requsetApp, IHomeServiceAppService homeServiceApp,
        UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> CheckoutRequest(int homeServiceId,CancellationToken ct)
        {
            ModelState.Clear();
            var homeService =await homeServiceApp.GetHomeServiceById(homeServiceId,ct);
            var model = new CreateRequestViewModel();
            model.HomeService=new HomeServiceViewModel
            {
                Id = homeServiceId,
                BasePrice=homeService.BasePrice,
                CategoryName=homeService.CategoryName,
                Description = homeService.Description
                ,ImagePath = homeService.ImagePath,
                Name = homeService.Name
            };
            model.HomeServiceId=homeServiceId;
            if(homeService == null)
    return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CheckoutRequest(CreateRequestViewModel model,CancellationToken ct)
        {
            var homeService = await homeServiceApp.GetHomeServiceById(model.HomeServiceId, ct);
            model.HomeService = new HomeServiceViewModel
            {
                Id = model.HomeServiceId,
                BasePrice = homeService.BasePrice,
                CategoryName = homeService.CategoryName,
                Description = homeService.Description
                ,
                ImagePath = homeService.ImagePath,
                Name = homeService.Name
            };
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var userId = Int32.Parse(_userManager.GetUserId(User));
            var request = new InputRequestDTO
            {
Title=model.Title,
Description=model.Description,
Images=model.Images,
CreatedAt=DateTime.Now,
AppointmentReadyDate=model.PersianDate.ConvertToGregorian(),
CustomerId= userId,
HomeServiceId=model.HomeServiceId

            };

            var result = await requsetApp.CreateRequest(ct, request);
            if (result)
            {
                TempData["Success"] = "درخواست شما با موفقیت ثبت شد. \nمنتظر پیشنهادات کارشناسان باشید.";

                return RedirectToAction("CheckoutRequest","Request" ,new { area = "Customer" , homeServiceId = model.HomeServiceId });

            }
            else
            {
                ViewBag.Error = "متاسفانه درخواست شما ثبت نشد";



                ModelState.AddModelError("", "ثبت درخواست ناموفق بود");

            }


            return View(model);
        }
    }
}
