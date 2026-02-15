using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Domain_Core.InfraContracts;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Services;
using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.Services;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class SuggestionAppService(ISuggestionService _sugsrv,IImageService _imageService,
        IFileService fileService,IExpertService _expertService,IRequestService requestService,ILogger<SuggestionAppService> logger):ISuggestionAppService
    {
        public async Task<PagedResult<OutputSuggestionDTO>> GetPagedSuggestionByRequestId(int id, int pageNumber, int pageSize, CancellationToken ct)
        {
           var result= await _sugsrv.GetPagedSuggestionByRequestId(id, pageNumber, pageSize, ct);
            foreach (var item in result.Items) 
            {
                item.ImagesPath = await _imageService.GetSuggestionImagesBySuggestionId(item.Id,ct);
                
            }
            return result;
        }
        public async Task<OutputSuggestionDTO?> GetApproveSuggestionByRequestId(int requestId, CancellationToken ct)
        {
         var result=   await _sugsrv.GetApproveSuggestionByRequestId(requestId, ct);
            if (result is not null)
            {
                result.ImagesPath= await _imageService.GetSuggestionImagesBySuggestionId(result.Id, ct);
            }
            return result;
        }
        public async Task<Result<bool>> CreateSuggestion(InputSuggestionDTO suggestion, CancellationToken ct)
        {
            var request = await requestService.GetRequestById(suggestion.RequestId,ct);
            var city = await _expertService.IsCityForExpert(suggestion.ExpertId, request.CityId.Value, ct);
            if (!city)
            {
                return Result<bool>.Failure("شهر شما با شهر کاربر درخواست کننده یکی نیست");
            }
            var skill = await _expertService.IsSkillForExpert(suggestion.ExpertId, suggestion.HomeServiceId, ct);
            if(!skill)
            {
                return Result<bool>.Failure("مهارت شما با مهارت مورد نیاز کاربر درخواست کننده یکی نیست");
            }
            var suggestionId = await _sugsrv.CreateSuggestion(suggestion,ct);
            if(suggestion.Images != null && suggestion.Images.Any())
            {
                var images = new List<SuggestionImageDTO>();
                foreach (var image in suggestion.Images)
                {
                    var imageDTO = new SuggestionImageDTO();
                    imageDTO.ImagePath = await fileService.Upload(image, "Suggestion", ct);
                    imageDTO.SuggestionId = suggestionId;
                    images.Add(imageDTO);
                }
                await _imageService.SetSuggestionImages(images, ct);
            }
            var count = await requestService.SuggestionCount(suggestion.RequestId, ct);
            if(count > 0)
            {
                await requestService.ChangeRequestToPendingSuggestionApproval(suggestion.RequestId, ct);
            }
            
            if (suggestionId > 0)
            {
                return Result<bool>.Success(suggestionId > 0,"عملیات موفقیت آمیز بود");
            }
            else
            {
                logger.LogWarning("Failed to create suggestion for ExpertId={ExpertId}", suggestion.ExpertId);
                return Result<bool>.Failure("عملیات موفقیت آمیز نبود");
            }
        }
     

    }
}
