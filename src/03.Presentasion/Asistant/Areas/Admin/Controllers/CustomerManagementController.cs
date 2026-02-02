using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerManagementController(ILogger<CustomerManagementController> logger ,
        ICityAppService _ctapp, ICustomerAppService _cstapp) : Controller
    {
        
        public async Task<IActionResult> ShowCustomers(CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var result = await _cstapp.GetPagedCustomers(pageNumber, pageSize, ct);
            var pagedResult = new PagedViewModel<CustomerOutputViewModel>();
            pagedResult.Items = result.Items.Select(c => new CustomerOutputViewModel
            {
                Id = c.Id,
                UserId = c.UserId,
                Adress = c.Adress,
                Email = c.Email
                ,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CityName=c.CityName,
                Ballance=c.Balance
            }).ToList();
            pagedResult.TotalCount = result.TotalCount;
            pagedResult.PageNumber = result.PageNumber;
            pagedResult.PageSize = result.PageSize;
            pagedResult.TotalPages = result.TotalPages;
            return View(pagedResult);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(CancellationToken ct, int id, int userId)
        {

            var result = await _cstapp.DeleteCustomer(ct, id, userId);
            if (result)

            {
                TempData["Succeed"] = "مشتری با موفقیت حذف شد";
               
            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";
                
            }

            return RedirectToAction("ShowCustomers");
        }
        [HttpGet]
        public async Task<IActionResult> EditCustomer(int id, int userId, CancellationToken ct)
        {
            var cities = (await _ctapp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            cities.Insert(0, new SelectListItem { Value = "", Text = "یک شهر انتخاب کنید" });
            var customer = await _cstapp.GetCustomerById(ct, id);

            var result = new EditCustomerViewModel
            {
                Adress = customer.Adress,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.Id,
                UserId = customer.UserId,
                CityId = customer.CityId == null ?  null :customer.CityId.Value,
                Cities = cities,
                ImagePath = customer.ImagePath,
                Ballance=customer.Balance


            };
            
           
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(EditCustomerViewModel model, CancellationToken ct)
        {
            var cities = (await _ctapp.GetAllCities(ct)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            }).ToList();
            cities.Insert(0, new SelectListItem { Value = "", Text = "یک شهر انتخاب کنید" });
            if (!ModelState.IsValid)
            {

                model.Cities = cities;
                return View(model);
            }
            model.Cities = cities;
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
                Image = model.Image,
                Balance=model.Ballance
            };
            var result = await _cstapp.UpdateCustomer(ct, editCustomer);
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
        public async Task<IActionResult> CreateCustomer(CancellationToken ct)
        {
            var cities = await _ctapp.GetAllCities(ct);
            var model = new CreateCustomerViewModel
            {
                Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString()
                ,
                    Text = c.Name
                })
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model, CancellationToken ct)
        {
            var cities = await _ctapp.GetAllCities(ct);
            if (!ModelState.IsValid)
            {

                model.Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString()
                    ,
                    Text = c.Name
                });
                return View(model);
            }
            model.Cities = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString()
                    ,
                Text = c.Name
            });
            var customer = new CreateCustomerDTO
            {
                Address = model.Adress,
                CityId = model.CityId,
                Email = model.Email,
                FirstName = model.FirstName
                ,
                Image = model.Image,
                LastName = model.LastName,
                Password = model.Password,
                Balance=model.Ballance
            };
            var result = await _cstapp.CreateCustomerByAdmin(customer, ct);
            if (result.Succeeded)
            {

                ViewBag.Succeed = "مشتری جدید ثبت شد";
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
