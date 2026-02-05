using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Infra_Cache.Contract;
using Asistant_Infra_File.Contract;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CategoryAppService(ICacheService cacheService,ICategoryService _catserv,ILogger<CategoryAppService> logger
        ,IFileService fileService
        ,IImageService _imageService, IHomeServiceService _hmService):ICategoryAppService
    {
        public async Task<IEnumerable<GetCategoryDTO>> GetAllCategories(CancellationToken ct)
        {
            var cacheKey = "GetAllCategories";
            var cached = cacheService.Get<IEnumerable<GetCategoryDTO>>(cacheKey);
            if (cached != null)
                return cached;
           var category= await _catserv.GetAllCategories(ct);
            cacheService.SetSliding(cacheKey, category, 30);
            return category;
        }
        public async Task<PagedResult<GetCategoryDTO>> GetPagedCategories(int pageNumber, int pageSize, CancellationToken ct)
        {
            var cacheKey = "GetPagedCategories";
            var cached = cacheService.Get<PagedResult<GetCategoryDTO>>(cacheKey);
            if (cached != null)
                return cached;
            var category = await _catserv.GetPagedCategories(pageNumber, pageSize, ct);
            cacheService.SetSliding(cacheKey, category, 30);
            return category;
        }
        public async Task<bool> DeleteCategory(int id,CancellationToken ct)
        {
            var imageId = await _catserv.GetCategoryImageId(id, ct);
            if (imageId !=null)
            {
                var image = await _imageService.GetImageByCategoryId(id,ct);
                await  fileService.Delete(image.ImagePath, ct);
                await _imageService.DeleteImage(image.Id, ct);
            }
            await _hmService.DeleteHomeServicesByCategoryId(id, ct);
           var deleted= await _catserv.DeleteCategory(id, ct);
            if(deleted)
            {
                cacheService.Remove("GetAllCategories");
                cacheService.Remove("GetPagedCategories");
            }
            return deleted;
        }

        public async Task<Result<bool>> CreateCategory(InputCategoryDTO categoryDTO,CancellationToken ct)
        {
            var exist= await _catserv.ExistCategoryName(categoryDTO.Name, ct);
            if(exist)
            {
                return Result<bool>.Failure("این دسته بندی پیش از این ثبت شده است");
            }
            var categoryId = await _catserv.CreateCategory(categoryDTO.Name, ct);
            if (categoryId > 0)
            {
                if (categoryDTO.Image != null)
                {
                    var imagePath = await fileService.Upload(categoryDTO.Image, "Category", ct);
                    var image = new ImageDTO { CategoryId = categoryId, ImagePath = imagePath };
                    var imageId = await _imageService.SetCategoryImage(image, ct);
                    await _catserv.UpdateImageId(categoryId, imageId, ct);

                }
                cacheService.Remove("GetAllCategories");
                cacheService.Remove("GetPagedCategories");
                return Result<bool>.Success(true, "دسته بندی با موفقیت ثبت شد");
            }
            return Result<bool>.Failure("عملیات با مشکلی مواجه شد");
        }
        public async Task<Result<bool>> UpdateCategory(InputCategoryDTO categoryDTO,CancellationToken ct)
        {
            var result = await _catserv.UpdateCategoryName(categoryDTO.Id, categoryDTO.Name, ct);
            if (categoryDTO.Image != null)
            {
                var imagePath = await fileService.Upload(categoryDTO.Image, "Category", ct);
                var currentImage = await _imageService.GetImageByCategoryId(categoryDTO.Id, ct);
              
                if (currentImage != null)
                {
                    await fileService.Delete(currentImage.ImagePath, ct);
                   
                   await _imageService.UpdateImagePathById(imagePath, currentImage.Id, ct);
                 
                }
                else
                {
                    var newImage = new ImageDTO
                    {
                        ImagePath = imagePath,
                        CategoryId = categoryDTO.Id,
                    };
                    var imageId = await _imageService.SetCategoryImage(newImage, ct);
                    await _catserv.UpdateImageId(categoryDTO.Id, imageId, ct);
                }
            }
            if (result)
            {
                cacheService.Remove("GetAllCategories");
                cacheService.Remove("GetPagedCategories");
                return Result<bool>.Success(result, "تغییرات با موفقیت ثبت شد");
            }
            return Result<bool>.Failure("ثبت تغییرات با مشکلی مواجه شد");
        }
        public async Task<GetCategoryDTO?> GetCategoryById(int id, CancellationToken ct)
       => await _catserv.GetCategoryById(id, ct);
    }
}
