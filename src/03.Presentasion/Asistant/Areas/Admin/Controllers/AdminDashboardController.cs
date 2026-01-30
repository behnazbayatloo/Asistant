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
    public class AdminDashboardController(UserManager<AppUser> _userManager
        ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var admin = new AdminViewModel
            {
                Id=user.Id,
                Email=user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName
            };
            return View(admin);
        }

        
        

    }
}
