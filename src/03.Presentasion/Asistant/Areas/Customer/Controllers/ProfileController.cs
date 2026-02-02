using Asistant.Areas.Customer.Models;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Asistant.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class ProfileController(ILogger<ProfileController> logger, UserManager<AppUser> _userManager,
        ICustomerAppService customerApp,ICityAppService cityApp) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var userId = Int32.Parse(_userManager.GetUserId(User));
           
            var model = new CustomerViewModel();
            if (userId >0)
            {
                var customer = await customerApp.GetCustomerByUserId(userId, ct);
                model = new CustomerViewModel
                {
                    Id = customer.Id,
                    UserId = userId,
                    Adress = customer.Adress,
                    CityId = customer.CityId,
                    CityName = customer.CityName,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    ImagePath = customer.ImagePath
                    ,
                    LastName = customer.LastName

                };
            }
           
            return View(model);
        }

        public  async Task<IActionResult> EditProfile(CancellationToken ct)
        {
            var cities = (await cityApp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            cities.Insert(0, new SelectListItem { Value = "", Text = "یک شهر انتخاب کنید" });
            var userId = Int32.Parse(_userManager.GetUserId(User));
            var model = new EditCustomerViewModel();
            if (userId >0)
            {
                var customer = await customerApp.GetCustomerByUserId(userId, ct);
                model = new EditCustomerViewModel
                {
                    Id = customer.Id,
                    UserId = userId,
                    Adress = customer.Adress,
                    CityId = customer.CityId,

                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    ImagePath = customer.ImagePath
                     ,
                    LastName = customer.LastName
                };
            }
          

                return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditCustomerViewModel model,CancellationToken ct)
        {
            var cities = (await cityApp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            model.Cities = cities;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var editCustomer = new UpdateCustomerDTO
            {
                Id = model.Id,
                Adress = model.Adress,
                CityId = model.CityId,
                Email = model.Email
                 ,
                FirstName = model.FirstName
                 ,
                LastName = model.LastName,
                Password = model.Password,
                UserId = model.UserId,
                Image = model.Image
            };
            var result = await customerApp.UpdateCustomer(ct, editCustomer);
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
        public async Task<IActionResult> DeleteProfile(int id,CancellationToken ct)
        {
            var userId = Int32.Parse(_userManager.GetUserId(User));
             var result = await customerApp.DeleteCustomer(ct, id, userId);
            if (result)

            {
                return RedirectToAction("Index", new {area =""});
            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";

            }
            return RedirectToAction("Index");

        }
    }
}
