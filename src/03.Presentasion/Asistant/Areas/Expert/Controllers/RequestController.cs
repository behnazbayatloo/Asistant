using Asistant.Areas.Expert.Models;
using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using Asistant_FrameWork.UIExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Asistant.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "Expert")]
    public class RequestController(IRequestAppService requestApp,ISuggestionAppService suggestionApp) : Controller
    {
        public async Task<IActionResult> ShowRequests(CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var expertId = Int32.Parse(User.FindFirst("ExpertId")?.Value);
            var requests = await requestApp.GetPagedRequestForExpert(expertId,pageNumber,pageSize,ct);
            var model = new PagedViewModel<RequestViewModel, int>();
            model.Items = requests.Items.Select(r => new RequestViewModel
            {
                Id = r.Id,
                CreatedAt=r.CreatedAt.ToPeString("yyyy/mm/dd"),
                CustomerId=r.CustomerId,
                CustomerName=r.CustomerName,
                Description = !string.IsNullOrEmpty(r.Description) ?
                (r.Description.Length >= 50 ? r.Description.Substring(0, 50) + "..." : r.Description) : "",
                HomeServiceId= r.HomeServiceId
                ,HomeServiceName=r.HomeServiceName,
                ImagesPath=r.ImagesPath,
                Status=r.Status,
                Title=r.Title

            }).ToList();
            model.TotalCount = requests.TotalCount;
            model.PageSize = requests.PageSize;
            model.PageNumber = requests.PageNumber;
            model.TotalPages=requests.TotalPages;

            return View(model);
        }
        public async Task<IActionResult> ShowDetail(int requestId,CancellationToken ct)
        {
            var request= await requestApp.GetRequestById(requestId,ct);
            var model = new RequestViewModel
            {
                CreatedAt=request.CreatedAt.ToPeString("yyyy/mm/dd"),
                CustomerId=request.CustomerId,
                CustomerName=request.CustomerName,
                Description = request.Description,
                HomeServiceId = request.HomeServiceId,
                HomeServiceName= request.HomeServiceName,
                Id=request.Id,
                ImagesPath=request.ImagesPath,
                Status=request.Status,
                Title=request.Title
            };
            return View(model);
        }

        public async Task<IActionResult> SendSuggestion(int requestId,CancellationToken ct)
        {
            var expertId = Int32.Parse(User.FindFirst("ExpertId")?.Value);
            var request = await requestApp.GetRequestById(requestId, ct);
            var model = new SuggestionViewModel
            {
                CustomerName=request.CustomerName,
                RequestCreatedAt=request.CreatedAt.ToPeString("yyyy/mm/dd"),
                ExpertId=expertId,
                RequestId=request.Id,
                RequestTitle=request.Title,
                HomeServiceId=request.HomeServiceId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SendSuggestion(SuggestionViewModel model,CancellationToken ct)
        {
            var expertId = Int32.Parse(User.FindFirst("ExpertId")?.Value);
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var suggest = new InputSuggestionDTO
            {
                CreatedAt=DateTime.Now,
                ExpertId = expertId,
                Description=model.Description,
                Images=model.Images,
                Price = model.Price,
                RequestId = model.RequestId,
                Title= model.Title,
                HomeServiceId=model.HomeServiceId
            };
            var result = await suggestionApp.CreateSuggestion(suggest, ct);
            if(result)
            {
                ViewBag.Succeed = "پیشنهاد شما ثبت گردید";
            }
            else
            {
                ViewBag.Error = "عملیات با مشکل مواجه شد";


            }
            return View(model);


        }
    }
}
