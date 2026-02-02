using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController(UserManager<AppUser> _userManager,IAppUserAppService _userApp
        ) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var userId =  _userManager.GetUserId(User);
            var result= await _userApp.GetAdminById(Int32.Parse(userId),ct);
            var admin = new AdminViewModel
            {
                Id= result.Id,
                Email= result.Email,
                FirstName= result.FirstName,
                LastName= result.LastName,
                Balance= result.Balance
            };
            return View(admin);
        }

        
        

    }
}
