using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Infra_Cache.Contract;
using Asistant_Infra_File.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class HomeServiceAppService(ICacheService cacheService,IHomeServiceService _hmsrv,IFileService fileservice,IImageService _imageservice,ILogger<HomeServiceAppService> logger):IHomeServiceAppService
    {
        public async Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct)
        {
            var cacheKey = "GetAllHomeServices";
            var cached = cacheService.Get<IEnumerable<GetHomeServiceDTO>>(cacheKey);
            if (cached != null)
                return cached;
            var services = await _hmsrv.GetAllHomeServices(ct);
            cacheService.SetSliding(cacheKey, services, 30);
            return services;
        }
        public async Task<PagedResult<GetHomeServiceDTO>> GetPagedHomeServices(int pageNumber, int pageSize, CancellationToken ct)
        {
            var cacheKey = "GetPagedHomeServices";
            var cached = cacheService.Get<PagedResult<GetHomeServiceDTO>>(cacheKey);
            if (cached != null)
                return cached;
            var services  = await _hmsrv.GetPagedHomeService(pageNumber, pageSize, ct);
            cacheService.SetSliding(cacheKey, services, 30);
            return services;
        }
        public async Task<bool> DeleteHomeService(int id,CancellationToken ct)
        {
            var categoryId = await _hmsrv.GetCategoryId(id, ct);
            var image=await _imageservice.GetImageByHomeServiceId(id, ct);
            if (image != null) 
            {
                await fileservice.Delete(image.ImagePath, ct);
                await _imageservice.DeleteImage(image.Id, ct);
            }
           var deleted= await _hmsrv.DeleteHomeService(id, ct);
            if(deleted)
            {
                cacheService.Remove("GetAllHomeServices");
                cacheService.Remove("GetPagedHomeServices");
                cacheService.Remove($"homeservices_category_{categoryId}");

            }
            return deleted;
        }
        public async Task<Result<bool>> CreateHomeService(InputHomeServiceDTO homeServiceDTO,CancellationToken ct)
        {
            var exist= await _hmsrv.ExistHomeService(homeServiceDTO.Name,ct);
            if(exist)
            {
                return Result<bool>.Failure("این سرویس پیش از این ثبت شده است");
            }
            var homeServiceId =await _hmsrv.CreateHomService(homeServiceDTO, ct);
            if (homeServiceId > 0)
            {
                if (homeServiceDTO.Image != null)
                {
                    var imagePath = await fileservice.Upload(homeServiceDTO.Image, "HomeService", ct);
                    var image = new ImageDTO { HomeServiceId = homeServiceId, ImagePath = imagePath };
                    int imageId = await _imageservice.SetHomeServiceImage(image, ct);
                    await _hmsrv.UpdateImageId(homeServiceId, imageId, ct);
                }
                cacheService.Remove("GetAllHomeServices");
                cacheService.Remove("GetPagedHomeServices");
                cacheService.Remove($"homeservices_category_{homeServiceDTO.CategoryId}");

                return Result<bool>.Success(true, "سرویس با موفقیت ثبت شد");
            }


            return Result<bool>.Failure("عملیات با مشکلی مواجه شد");
        }
        public async Task<Result<bool>> UpdateHomeService(InputHomeServiceDTO homeServiceDTO,CancellationToken ct)
        {
          var result=  await _hmsrv.UpdateHomeService(homeServiceDTO, ct);
            if (homeServiceDTO.Image != null)
            {
                var image = await _imageservice.GetImageByHomeServiceId(homeServiceDTO.Id, ct);
                var imagePath = await fileservice.Upload(homeServiceDTO.Image, "HomeService", ct);
                if (image != null)
                {
                    await fileservice.Delete(image.ImagePath, ct);
                    await _imageservice.UpdateImagePathById(imagePath, image.Id, ct);
                }
                else
                {
                    var newImage = new ImageDTO
                    {
                        HomeServiceId = homeServiceDTO.Id,
                        ImagePath = imagePath
                    };
                    var imageId = await _imageservice.SetHomeServiceImage(newImage, ct);
                    await _hmsrv.UpdateImageId(homeServiceDTO.Id, imageId, ct);
                }
            }
            if (result)
            {
                cacheService.Remove("GetAllHomeServices");
                cacheService.Remove("GetPagedHomeServices");
                cacheService.Remove($"homeservices_category_{homeServiceDTO.CategoryId}");
                return Result<bool>.Success(result,"تغییرات با موفقیت ثبت شد");
            }
            return Result<bool>.Failure("ثبت تغییرات با مشکلی مواجه شد");
        }
        public async Task<IEnumerable<GetHomeServiceDTO>> GetHomeServiceByCategoryId(int categoryId, CancellationToken ct)
        {
            var cacheKey = $"homeservices_category_{categoryId}";
            var cached = cacheService.Get<IEnumerable<GetHomeServiceDTO>>(cacheKey); 
            if (cached != null)
                return cached;

            var services=  await _hmsrv.GetHomeServicesByCategoryId(categoryId, ct);
            cacheService.SetSliding(cacheKey, services, 30); 
            return services;
        }
        public async Task<GetHomeServiceDTO?> GetHomeServiceById(int id, CancellationToken ct)
            => await _hmsrv.GetHomeServiceById(id, ct);

    
    }
}
