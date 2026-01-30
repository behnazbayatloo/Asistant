using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentManagementController (ILogger<CommentManagementController> logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
