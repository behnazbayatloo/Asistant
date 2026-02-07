using Asistant.Areas.Admin.Models;
using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_FrameWork.UIExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asistant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestManagementController (IRequestAppService _requestApp,
        ISuggestionAppService _suggestionApp,ILogger<RequestManagementController> logger) : Controller
    {
        public async Task<IActionResult> ShowRequests(CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var requests= await _requestApp.GetPagedRequest(pageNumber, pageSize,ct);
            var model = new PagedViewModel<RequestViewModel,int>();
            model.Items = requests.Items.Select(r => new RequestViewModel
            {
                Id = r.Id,
                AppointmentReadyDate = r.AppointmentReadyDate.ToPeString("yyyy/MM/dd"),
               CompletedDate=r.CompletedDate.ToPeString("yyyy/MM/dd"),
               CreatedAt = r.CreatedAt.ToPeString("yyyy/MM/dd"),
               CustomerId = r.CustomerId,
               CustomerName= r.CustomerName,
               Description= r.Description,
               HomeServiceName= r.HomeServiceName,
               HomeServiceId = r.HomeServiceId,
               Status=r.Status,
               SuggesstionCount=r.SuggesstionCount,
               
               Title =r.Title,
               VerifyExpertDate=r.VerifyExpertDate.ToPeString("yyyy/MM/dd"),
               SuggestionsId=r.SuggestionsId,


            }).ToList();
            model.TotalPages=requests.TotalPages;
            model.TotalCount = requests.TotalCount;
            model.PageSize=requests.PageSize;
            model.PageNumber=requests.PageNumber;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RejectRequest(int id,CancellationToken ct)
        {
            var result = await _requestApp.RejectRequest(id,ct);

          if(result)
            {
                TempData["Succeed"] = "درخواست توسط شما رد شد";
            }

            else
            {
                TempData["Error"]= "عملیات با مشکل مواجه شد";
            }

              return RedirectToAction("ShowRequests");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRequest(int id, CancellationToken ct)
        {
            var result =await _requestApp.DeleteRequest(id,ct) ;

            if (result)
            {
                TempData["Succeed"] = "درخواست توسط شما حذف گردید";
            }

            else
            {
                TempData["Error"] = "عملیات با مشکل مواجه شد";
            }

            return RedirectToAction("ShowRequests");
        }
        
        public async Task<IActionResult> ShowSuggestions(int id, CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var pagedResult = await _suggestionApp.GetPagedSuggestionByRequestId(id, pageNumber, pageSize, ct);
            var request = await _requestApp.GetRequestById(id, ct);
            var model = new PagedViewModel<SuggestionViewModel, RequestViewModel>();
            if(request is not null)
            {
                model.MyProp = new RequestViewModel
                {
                    AppointmentReadyDate = request.AppointmentReadyDate.ToPeString("yyyy/MM/dd"),
                    CompletedDate = request.CompletedDate.ToPeString("yyyy/MM/dd"),
                    CreatedAt = request.CreatedAt.ToPeString("yyyy/MM/dd"),
                    CustomerName = request.CustomerName,
                    Description = request.Description
               ,
                    HomeServiceName = request.HomeServiceName
               ,
                    ImagesPath = request.ImagesPath,
                    Title = request.Title,
                    Status = request.Status,
                    SuggesstionCount = request.SuggesstionCount,
                    SuggestionsId=request.SuggestionsId,
                    VerifyExpertDate = request.VerifyExpertDate.ToPeString("yyyy/MM/dd")

                };
            }
           
            model.Items = pagedResult.Items.Select(s => new SuggestionViewModel
            {
                Id=s.Id,
                CreatedAt=s.CreatedAt.ToPeString("yyyy/MM/dd"),
                Description=s.Description,
                ExpertId= s.ExpertId
               ,ExpertName= s.ExpertName
               ,ImagesId=s.ImagesId,
                ImagesPath=s.ImagesPath,
                Price=s.Price,
                RequestId=s.RequestId,
                Status=s.Status,
                Title=s.Title

            }).ToList();
            model.TotalPages=pagedResult.TotalPages;
            model.TotalCount=pagedResult.TotalCount;
            model.PageNumber=pagedResult.PageNumber;
            model.PageSize=pagedResult.PageSize;

            return View(model);

        }
       


    }
}
