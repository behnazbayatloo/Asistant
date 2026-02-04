using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class SuggestionAppService(ISuggestionService _sugsrv,IImageService _imageService,ILogger<SuggestionAppService> logger):ISuggestionAppService
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

    }
}
