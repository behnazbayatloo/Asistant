using Asistant_Domain_Core.ImageAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.ImageAgg.Data
{
    public interface IImageRepository
    {
        Task<bool> DeleteImage(int imageId, CancellationToken ct);
       
       
        Task<int> SetUserProfile(ProfileImageDTO Image, CancellationToken ct);
        Task<bool> UpdateImagePathById(string path, int id, CancellationToken ct);
        Task<int> SetHomeServiceImage(ImageDTO imageDTO, CancellationToken ct);
        Task<int> SetCategoryImage(ImageDTO imageDTO, CancellationToken ct);
      
        Task<ProfileImageDTO?> GetImageByExpertId(int userId, CancellationToken ct);
        Task<ImageDTO?> GetImageByCategoryId(int id, CancellationToken ct);
        Task<ImageDTO?> GetImageByHomeServiceId(int id, CancellationToken ct);
        Task<ProfileImageDTO?> GetImageByCustomerId(int userId, CancellationToken ct);
        Task<bool> SetRequestImage(List<RequestImageDTO> requestImages, CancellationToken ct);
        Task<List<string>> GetSuggestionImagesBySuggestionId(int id, CancellationToken ct);
        Task<List<string>> GetRequestImagesByRequestId(int id, CancellationToken ct);
        Task<bool> SetSuggestionImages(List<SuggestionImageDTO> suggestionImageDTOs, CancellationToken ct);
    }
}
