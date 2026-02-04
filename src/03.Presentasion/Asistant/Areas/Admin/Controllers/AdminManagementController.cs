using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminManagementController(IAppUserAppService _appUserAppService
        ) : Controller
    {
       
             public async Task<IActionResult> ShowAdmins(CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var result = await _appUserAppService.GetAdminsPagedResult(pageNumber, pageSize, ct);
            var pagedResult = new PagedViewModel<AdminViewModel,int>();
            pagedResult.Items = result.Items.Select(a => new AdminViewModel
            {
                Id = a.Id,
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Balance=a.Balance
            }).ToList();
            pagedResult.PageNumber = pageNumber;
            pagedResult.PageSize = pageSize;
            pagedResult.TotalCount = result.TotalCount;
            pagedResult.TotalPages = result.TotalPages;
            return View(pagedResult);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAdmin(CancellationToken ct, int id)
        {
            var result = await _appUserAppService.DeleteAdmin(ct, id);
            if (result)

            {
                TempData["Succeed"] = "ادمین با موفقیت حذف شد";
                return RedirectToAction("ShowAdmins");
            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";
                return RedirectToAction("ShowAdmins");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditAdmin(int id, CancellationToken ct)
        {
           var admin= await _appUserAppService.GetAdminById(id, ct);

            var result = new EditAdminViewModel
            {
               
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Id = admin.Id,
               Balance = admin.Balance


            };


            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(EditAdminViewModel model, CancellationToken ct)
        {
            
            if (!ModelState.IsValid)
            {

                return View(model);
            }
          
            var editAdmin = new AppUserFieldsDTO
            {
                Id = model.Id,
              
                Email = model.Email
                ,
                FirstName = model.FirstName
                ,
                LastName = model.LastName,
                Password = model.Password,
                Ballance=model.Balance
            };
            var result = await _appUserAppService.UpdateAdmin(editAdmin);
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
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(CreateAdminViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var admin = new CreateAdminDTO
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Balance = model.Balance
            };
            var result= await _appUserAppService.CreateAdmin(ct,admin);
           
                if (result.Succeeded)
                {

                    ViewBag.Succeed = "admin جدید ثبت شد";
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
