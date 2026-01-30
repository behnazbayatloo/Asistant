using Asistant_Domain_Core.ImageAgg.Data;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class ImageService(IImageRepository _imgrepo, ILogger<ImageService> logger) : IImageService
    {
        public async Task<int> SetImageProfile(ProfileImageDTO profileImageDTO, CancellationToken ct)
             => await _imgrepo.SetUserProfile(profileImageDTO, ct);
        public async Task<bool> DeleteImage(int imageId, CancellationToken ct)
             => await _imgrepo.DeleteImage(imageId, ct);

        public async Task<bool> UpdateImagePathById(string path, int id, CancellationToken ct)
            => await _imgrepo.UpdateImagePathById(path, id, ct);

        public async Task<int> SetHomeServiceImage(ImageDTO imageDTO, CancellationToken ct)
            => await _imgrepo.SetHomeServiceImage(imageDTO, ct);
        public async Task<int> SetCategoryImage(ImageDTO imageDTO, CancellationToken ct)
            => await _imgrepo.SetCategoryImage(imageDTO, ct);
        public async Task<ProfileImageDTO?> GetImageByExpertId(int userId, CancellationToken ct)
            => await _imgrepo.GetImageByExpertId(userId, ct);
        public async Task<ImageDTO?> GetImageByCategoryId(int id, CancellationToken ct)
            => await _imgrepo.GetImageByCategoryId(id, ct);
        public async Task<ImageDTO?> GetImageByHomeServiceId(int id, CancellationToken ct)
            => await _imgrepo.GetImageByHomeServiceId(id, ct);
        public async Task<ProfileImageDTO?> GetImageByCustomerId(int userId, CancellationToken ct)
            => await _imgrepo.GetImageByCustomerId(userId, ct);
        public async Task<bool> SetRequestImage(List<RequestImageDTO> requestImages, CancellationToken ct)
            => await _imgrepo.SetRequestImage(requestImages, ct);
        }
}
