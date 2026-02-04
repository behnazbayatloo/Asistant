using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_FrameWork.UIExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentManagementController(ICommentAppService _commentApp, ILogger<CommentManagementController> logger) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct, int pageNumber = 1, int pageSize = 2, bool? showAll = null)
        {
           
            var result = await _commentApp.GetPagedComment(pageNumber, pageSize, ct, showAll);

            
            var pagedResult = new PagedViewModel<CommentViewModel,int>();

           
            pagedResult.Items = result.Items.Select(r => new CommentViewModel
            {
               
                CreatedAt = r.CreatedAt.ToPeString("yyyy/mm/dd"),

                CustomerId = r.CustomerId,
                CustomerName = r.CustomerName,
                Description = r.Description,
                ExpertId = r.ExpertId,
                ExpertName = r.ExpertName,
                HomeServiceId = r.HomeServiceId,
                HomeServiceName = r.HomeServiceName,
                Id = r.Id,
                Rate = r.Rate,
                RequestId = r.RequestId,
                Status = r.Status,
                Title = r.Title
            }).ToList();

            
            pagedResult.TotalCount = result.TotalCount;
            pagedResult.PageNumber = result.PageNumber;
            pagedResult.PageSize = result.PageSize;
            pagedResult.TotalPages = result.TotalPages;

           
            return View(pagedResult);
        }
        [HttpPost]
        public async Task<IActionResult> AcceptComment(int id,CancellationToken ct)
        {
            var result = await _commentApp.AcceptComment(id, ct);
            if (result)
            {
                TempData["Succeed"] = "کامنت تایید شد";
            }
            else
            {
                TempData["Error"] = "عملیات با مشکل مواجه شد";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RejectComment( int id,CancellationToken ct)
        {
            var result = await _commentApp.RejectComment(id, ct);
            if (result)
            {
                TempData["Succeed"] = "کامنت رد شد";
            }
            else
            {
                TempData["Error"] = "عملیات با مشکل مواجه شد";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id,CancellationToken ct)
        {
            var result = await _commentApp.DeleteComment(id, ct);
            if(result)
            {
                TempData["Succeed"] = "کامنت با موفقیت حذف گردید";
            }
            else
            {
                TempData["Error"] = "عملیات با مشکل مواجه شد";
            }
                return RedirectToAction("Index");
        }
    }
}