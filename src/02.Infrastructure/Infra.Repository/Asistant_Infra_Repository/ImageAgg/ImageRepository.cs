using Asistant_Domain_Core.CommentAgg.Enum;
using Asistant_Domain_Core.ImageAgg.Data;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.ImageAgg.Enum;
using Asistant_Infra_Db_Sql.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.ImageAgg
{
    public class ImageRepository(ApplicationDbContext _dbcontext):IImageRepository
    {
        public async Task<int> SetUserProfile(ProfileImageDTO imageDTO, CancellationToken ct)
        {
            var image = new Image
            {
                CustomerId = imageDTO.CustomerId,
                ExpertId = imageDTO.ExpertId,
                ImagePath = imageDTO.ImagePath,
                ImageCategory= ImageEnum.UserProfile
            };
            await _dbcontext.Images.AddAsync(image, ct);
            await _dbcontext.SaveChangesAsync();
            return image.Id;
        }
        public async Task<int> SetHomeServiceImage(ImageDTO imageDTO, CancellationToken ct)
        {
            var image = new Image
            {
                ImagePath = imageDTO.ImagePath,
               
                HomeServiceId = imageDTO.HomeServiceId,
                ImageCategory = ImageEnum.HomeService
            };
            await _dbcontext.Images.AddAsync(image, ct);
            await _dbcontext.SaveChangesAsync();
            return image.Id;
        }
        public async Task<int> SetCategoryImage(ImageDTO imageDTO, CancellationToken ct)
        {
            var image = new Image
            {
                ImagePath = imageDTO.ImagePath,
                CategoryId = imageDTO.CategoryId,
                
                ImageCategory = ImageEnum.Category
            };
            await _dbcontext.Images.AddAsync(image, ct);
            await _dbcontext.SaveChangesAsync();
            return image.Id;
        }
        public async Task<bool> DeleteImage(int imageId, CancellationToken ct) 
        {
          return  await _dbcontext.Images.Where(i=>i.Id==imageId)
                .ExecuteDeleteAsync(ct)>0;

        }
        public async Task<ProfileImageDTO?> GetImageByCustomerId(int userId, CancellationToken ct)
        {
            return await _dbcontext.Images.AsNoTracking()
                .Where(i => i.CustomerId == userId )
                .Select(i => new ProfileImageDTO
                {
                    Id=i.Id,
                    ImagePath=i.ImagePath
                }).FirstOrDefaultAsync(ct);
        }
        public async Task<ProfileImageDTO?> GetImageByExpertId(int userId, CancellationToken ct)
        {
            return await _dbcontext.Images.AsNoTracking()
                .Where(i => i.ExpertId == userId)
                .Select(i => new ProfileImageDTO
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath
                }).FirstOrDefaultAsync(ct);
        }
        public async Task<ImageDTO?> GetImageByHomeServiceId(int id,CancellationToken ct)
        {
            return await _dbcontext.Images.AsNoTracking()
               .Where(i =>  i.HomeServiceId == id)
               .Select(i => new ImageDTO
               {
                   Id = i.Id,
                   ImagePath = i.ImagePath
               }).FirstOrDefaultAsync(ct);
        }
        public async Task<ImageDTO?> GetImageByCategoryId(int id, CancellationToken ct)
        {
            return await _dbcontext.Images.AsNoTracking()
               .Where(i => i.CategoryId == id)
               .Select(i => new ImageDTO
               {
                   Id = i.Id,
                   ImagePath = i.ImagePath
               }).FirstOrDefaultAsync(ct);
        }
        public async Task<bool> UpdateImagePathById(string path,int id,CancellationToken ct)
        {
            return await _dbcontext.Images.Where(i=>i.Id==id)
                .ExecuteUpdateAsync(setter=> setter.SetProperty(i=>i.ImagePath,path),ct)>0;
        }
        public async Task<ImageDTO?> GetServicesImageById(int id,CancellationToken ct)
        {
            return await _dbcontext.Images.Where(i => i.CategoryId == id || i.HomeServiceId == id)
                .Select(i => new ImageDTO
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,

                }).FirstOrDefaultAsync(ct);

        }
        public async Task<bool> SetRequestImage(List<RequestImageDTO> requestImages,CancellationToken ct)
        {
            var images = new List<Image>();
            foreach (var imageDto in requestImages)
            {
                var image = new Image
                {
                    ImagePath = imageDto.ImagePath,
                    RequestId = imageDto.RequestId,
                    ImageCategory=ImageEnum.Request
                };
                images.Add(image);
            }
            await _dbcontext.Images.AddRangeAsync(images, ct);
          return  await _dbcontext.SaveChangesAsync(ct)>0;
        }
        public async Task<bool> SetSuggestionImages(List<SuggestionImageDTO> suggestionImageDTOs,CancellationToken ct)
        {
            var images = new List<Image>();
            foreach (var imageDTO in  suggestionImageDTOs)
            {
                var image = new Image
                {
                    ImagePath = imageDTO.ImagePath,
                    SuggestionId = imageDTO.SuggestionId,
                    ImageCategory = ImageEnum.Suggestion
                };
                images.Add(image);
            }
            await _dbcontext.Images.AddRangeAsync(images, ct);
            return await _dbcontext.SaveChangesAsync(ct) > 0;
        }
        public async Task<List<string>> GetSuggestionImagesBySuggestionId(int id,CancellationToken ct)
        {
            return await _dbcontext.Images
                .Where(i => i.SuggestionId == id)
                .Select(i => i.ImagePath)
                .ToListAsync(ct);
        }
        public async Task<List<string>> GetRequestImagesByRequestId(int id, CancellationToken ct) 
        {
            return await _dbcontext.Images
                    .Where(i => i.RequestId == id)
                    .Select(i => i.ImagePath)
                    .ToListAsync(ct);
        }

       
    }
}
