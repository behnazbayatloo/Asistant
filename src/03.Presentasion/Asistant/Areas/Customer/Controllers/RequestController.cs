using Asistant.Areas.Customer.Models;
using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_Domain_Core.CommentAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_FrameWork.UIExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Asistant.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class RequestController(ILogger<RequestController> logger,
        IRequestAppService requsetApp, IHomeServiceAppService homeServiceApp,
        ISuggestionAppService suggestionApp,
        ICommentAppService commentApp,
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
        public async Task<IActionResult> ShowInProgressRequests( CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
           
            var customerId = Int32.Parse(User.FindFirst("CustomerId")?.Value);
          
            var model = new PagedViewModel<RequestViewModel, int>();
            if (customerId > 0)
            {
                var requests= await requsetApp.GetPagedInProgressRequestByCustomerId(customerId, pageNumber,pageSize,ct);


                model.Items = requests.Items.Select(r => new RequestViewModel
                {
                    Id = r.Id,
                    AppointmentReadyDate = r.AppointmentReadyDate.ToPeString("yyyy/MM/dd"),
                    CompletedDate = r.CompletedDate.ToPeString("yyyy/MM/dd"),
                    CreatedAt = r.CreatedAt.ToPeString("yyyy/MM/dd"),
                    CustomerId = r.CustomerId,
                    CustomerName = r.CustomerName,
                    Description = r.Description,
                    HomeServiceName = r.HomeServiceName,
                    HomeServiceId = r.HomeServiceId,
                    Status = r.Status,
                    SuggesstionCount = r.SuggesstionCount,

                    Title = r.Title,
                    VerifyExpertDate = r.VerifyExpertDate.ToPeString("yyyy/MM/dd"),
                    SuggestionsId = r.SuggestionsId,


                }).ToList();
                model.TotalPages = requests.TotalPages;
                model.TotalCount = requests.TotalCount;
                model.PageSize = requests.PageSize;
                model.PageNumber = requests.PageNumber;
              
            }
            return View(model);
        }
        public async Task<IActionResult> ShowDoneRequests( CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var customerId = Int32.Parse(User.FindFirst("CustomerId")?.Value);
            var model = new PagedViewModel<RequestViewModel, int>();
            if (customerId > 0)
            {
                var requests = await requsetApp.GetPagedDoneRequestByCustomerId(customerId, pageNumber, pageSize, ct);


                model.Items = requests.Items.Select(r => new RequestViewModel
                {
                    Id = r.Id,
                    AppointmentReadyDate = r.AppointmentReadyDate.ToPeString("yyyy/MM/dd"),
                    CompletedDate = r.CompletedDate.ToPeString("yyyy/MM/dd"),
                    CreatedAt = r.CreatedAt.ToPeString("yyyy/MM/dd"),
                    CustomerId = r.CustomerId,
                    CustomerName = r.CustomerName,
                    Description = r.Description,
                    HomeServiceName = r.HomeServiceName,
                    HomeServiceId = r.HomeServiceId,
                    Status = r.Status,
                    SuggesstionCount = r.SuggesstionCount,
                    CommentId=r.CommentId,
                    Title = r.Title,
                    VerifyExpertDate = r.VerifyExpertDate.ToPeString("yyyy/MM/dd"),
                    SuggestionsId = r.SuggestionsId,


                }).ToList();
                model.TotalPages = requests.TotalPages;
                model.TotalCount = requests.TotalCount;
                model.PageSize = requests.PageSize;
                model.PageNumber = requests.PageNumber;

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRequest(int requestId,CancellationToken ct)
        {
            var result = await requsetApp.DeleteRequestByRequestId(requestId, ct);

            if (result)
            {
                TempData["Succeed"] = "درخواست با موفقیت حذف گردید";
               }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";
            }
            return RedirectToAction("ShowInProgressRequests");
       
        }
        public async Task<IActionResult> ShowSuggestions(int requestId,CancellationToken ct, int pageNumber = 1, int pageSize = 2)
        {
            var pagedResult = await suggestionApp.GetPagedSuggestionByRequestId(requestId, pageNumber, pageSize, ct);
            var request = await requsetApp.GetRequestById(requestId, ct);
            var model = new PagedViewModel<SuggestionViewModel, RequestViewModel>();
            if (request is not null)
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
                    SuggestionsId = request.SuggestionsId,
                    VerifyExpertDate = request.VerifyExpertDate.ToPeString("yyyy/MM/dd")

                };
            }

            model.Items = pagedResult.Items.Select(s => new SuggestionViewModel
            {
                Id = s.Id,
                CreatedAt = s.CreatedAt.ToPeString("yyyy/MM/dd"),
                Description = s.Description,
                ExpertId = s.ExpertId
               ,
                ExpertName = s.ExpertName
               ,
                ImagesId = s.ImagesId,
                ImagesPath = s.ImagesPath,
                Price = s.Price,
                RequestId = s.RequestId,
                Status = s.Status,
                Title = s.Title

            }).ToList();
            model.TotalPages = pagedResult.TotalPages;
            model.TotalCount = pagedResult.TotalCount;
            model.PageNumber = pagedResult.PageNumber;
            model.PageSize = pagedResult.PageSize;

            return View(model);
        }
        //public async Task<IActionResult> ApproveSuggestion(int suggestionId, CancellationToken ct)
        //{
            

        //}

        public async Task<IActionResult> AddComment(int requestId, CancellationToken ct)
        {
            var request = await requsetApp.GetRequestById(requestId, ct);
            var suggestion= await suggestionApp.GetApproveSuggestionByRequestId(requestId,ct);
            var model = new CreateCommentViewModel();
            model.Suggestion = new SuggestionViewModel
            {
                CreatedAt=suggestion.CreatedAt.ToPeString("yyyy/mm/dd"),
                Description=suggestion.Description,
                ExpertId=suggestion.ExpertId,
               ExpertName= suggestion.ExpertName,
               Id= suggestion.Id,
               ImagesPath=suggestion.ImagesPath,
               Price=suggestion.Price,
               RequestId=suggestion.RequestId,
               Status= suggestion.Status,
               Title=suggestion.Title
               

            };
            model.Request = new RequestViewModel
            {
                Id = request.Id,
                AppointmentReadyDate = request.AppointmentReadyDate.ToPeString("yyyy/mm/dd"),
                CompletedDate = request.CompletedDate.ToPeString("yyyy/mm/dd"),
                CreatedAt = request.CreatedAt.ToPeString("yyyy/mm/dd"),
                CustomerName=request.CustomerName,
                CustomerId=request.CustomerId,
                Description=request.Description,
                HomeServiceId= request.HomeServiceId,
                HomeServiceName=request.HomeServiceName,
                ImagesPath = request.ImagesPath,
                Title=request.Title,
                VerifyExpertDate=request.VerifyExpertDate.ToPeString("yyyy/mm/dd"),
                Status=request.Status
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentViewModel inputComment, CancellationToken ct)
        {
            var request = await requsetApp.GetRequestById(inputComment.Request.Id, ct);
            var suggestion = await suggestionApp.GetApproveSuggestionByRequestId(inputComment.Request.Id, ct);

            inputComment.Suggestion = new SuggestionViewModel
            {
                CreatedAt = suggestion.CreatedAt.ToPeString("yyyy/mm/dd"),
                Description = suggestion.Description,
                ExpertId = suggestion.ExpertId,
                ExpertName = suggestion.ExpertName,
                Id = suggestion.Id,
                ImagesPath = suggestion.ImagesPath,
                Price = suggestion.Price,
                RequestId = suggestion.RequestId,
                Status = suggestion.Status,
                Title = suggestion.Title


            };
            inputComment.Request = new RequestViewModel
            {
                Id = request.Id,
                AppointmentReadyDate = request.AppointmentReadyDate.ToPeString("yyyy/mm/dd"),
                CompletedDate = request.CompletedDate.ToPeString("yyyy/mm/dd"),
                CreatedAt = request.CreatedAt.ToPeString("yyyy/mm/dd"),
                CustomerName = request.CustomerName,
                CustomerId = request.CustomerId,
                Description = request.Description,
                HomeServiceId = request.HomeServiceId,
                HomeServiceName = request.HomeServiceName,
                ImagesPath = request.ImagesPath,
                Title = request.Title,
                VerifyExpertDate = request.VerifyExpertDate.ToPeString("yyyy/mm/dd"),
                Status = request.Status
            };
            if(!ModelState.IsValid)
            {
                return View(inputComment);
            }
            var comment = new InputCommentDTO
            {
                CreatedAt=DateTime.Now,
                CustomerId=inputComment.Request.CustomerId,
                Description=inputComment.Comment.Description,
                ExpertId=inputComment.Suggestion.ExpertId,
                HomeServiceId=inputComment.Request.HomeServiceId,
                Rate=inputComment.Comment.Rate,
                RequestId=inputComment.Request.Id,
                Title=inputComment.Comment.Title
            };
            var result = await commentApp.CreateComment(comment, ct);
            if (result)
            {

                ViewBag.Succeed ="کامنت با موفقیت ثبت شد";
            }
            else
            {


                ViewBag.Error = "عملیات با مشکل مواجه شد";
            }
            return View (inputComment);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId,int id,CancellationToken ct)
        {
            var result =await commentApp.DeleteComment(commentId,ct) ;
            if (result)
            {
                TempData["Succeed"] = "درخواست با موفقیت حذف گردید";
            }
            else
            {
                TempData["Error"] = "عملیات موفقیت آمیز نبود";
            }
            return RedirectToAction("ShowComment",new { requestId= id });
        }
        public async Task<IActionResult> ShowComment(int requestId,CancellationToken ct)
        {
            var comment = await commentApp.GetCommentByRequestId(requestId, ct);
            var model = new CommentViewModel
            {
                Id=comment.Id,
                    CreatedAt=comment.CreatedAt.ToPeString("yyyy/mm/dd"),
                    CustomerName=comment.CustomerName,
                    Description=comment.Description,
                    ExpertName=comment.ExpertName,
                    HomeServiceName=comment.HomeServiceName,
                    Rate=comment.Rate,
                    Status=comment.Status,
                    Title=comment.Title,
                    RequestId=requestId

            };
            return View(model);
        }
    }
}
