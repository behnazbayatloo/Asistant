using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.RequestAgg.Services;
using Asistant_Infra_File.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class RequestAppService(IRequestService _rqsrv, IFileService fileService, IImageService _imageService, ILogger<RequestAppService> logger) : IRequestAppService
    {
        public async Task<bool> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO)
        {
            var requestId = await _rqsrv.CreateRequest(ct, requestDTO);

            if (requestDTO.Images != null && requestDTO.Images.Any())
            {
                var images = new List<RequestImageDTO>();
                foreach (var image in requestDTO.Images)
                {
                    var imageDTO = new RequestImageDTO();
                    imageDTO.ImagePath = await fileService.Upload(image, "Request", ct);
                    imageDTO.RequestId = requestId;
                    images.Add(imageDTO);
                }
                await _imageService.SetRequestImage(images, ct);
            }
            return requestId > 0;
        }


        public async Task<PagedResult<OutputRequestDTO>> GetPagedRequest(int pageNumber, int pageSize, CancellationToken ct)
        => await _rqsrv.GetPagedRequest(pageNumber, pageSize, ct);
        public async Task<bool> RejectRequest(int id, CancellationToken ct)
      => await _rqsrv.RejectRequest(id, ct);
        public async Task<bool> DeleteRequest(int id, CancellationToken ct)
            => await _rqsrv.DeleteRequest(id, ct);
        public async Task<OutputRequestDTO?> GetRequestById(int id, CancellationToken ct)
        {
            var result = await _rqsrv.GetRequestById(id, ct);
            if(result is not null)
            {
                result.ImagesPath = await _imageService.GetRequestImagesByRequestId(id, ct);
            }
            
            return result;
        }
        public async Task<PagedResult<OutputRequestDTO>> GetPagedInProgressRequestByCustomerId(int id, int pageNumber, int pageSize, CancellationToken ct)
          => await _rqsrv.GetPagedInProgressRequestByCustomerId(id, pageNumber, pageSize, ct);
        public async Task<PagedResult<OutputRequestDTO>> GetPagedDoneRequestByCustomerId(int id, int pageNumber, int pageSize, CancellationToken ct)
            => await _rqsrv.GetPagedDoneRequestByCustomerId(id, pageNumber, pageSize, ct);
        public async Task<bool> DeleteRequestByRequestId(int requestId, CancellationToken ct)
           => await _rqsrv.DeleteRequestByRequestId(requestId, ct);

    }
}
