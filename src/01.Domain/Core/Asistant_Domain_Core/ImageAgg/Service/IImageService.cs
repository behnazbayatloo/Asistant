using Asistant_Domain_Core.ImageAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.ImageAgg.Service
{
    public interface IImageService
    {
        Task<bool> DeleteImage(int imageId, CancellationToken ct);
        Task<ImageDTO?> GetImageByCategoryId(int id, CancellationToken ct);
        Task<ProfileImageDTO?> GetImageByCustomerId(int userId, CancellationToken ct);
        Task<ProfileImageDTO?> GetImageByExpertId(int userId, CancellationToken ct);
        Task<ImageDTO?> GetImageByHomeServiceId(int id, CancellationToken ct);
      
    
        Task<int> SetCategoryImage(ImageDTO imageDTO, CancellationToken ct);
        Task<int> SetHomeServiceImage(ImageDTO imageDTO, CancellationToken ct);
        Task<int> SetImageProfile(ProfileImageDTO profileImageDTO, CancellationToken ct);
        Task<bool> SetRequestImage(List<RequestImageDTO> requestImages, CancellationToken ct);
        Task<bool> UpdateImagePathById(string path, int id, CancellationToken ct);
    }
}
