using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.Service;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Domain_Core.RequestAgg.Services;
using Asistant_Domain_Core.SuggestionAgg.Services;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UserAgg.Services;
using Asistant_Infra_File.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class ExpertAppService(UserManager<AppUser> _userManager,
        SignInManager<AppUser> _signInManager,
        RoleManager<IdentityRole<int>> _roleManager,IAppUserService _appsrv,
         ICommentService _cmtsrv
        , ISuggestionService _sgsrv, 
         IFileService fileService,
         IImageService _imagesrv,
         IExpertService _expsrv,
         IHomeServiceService _homeService
         ,ILogger<ExpertAppService> logger) : IExpertAppService
    {
        public async Task<PagedResult<OutputExpertDTO>> GetPagedExperts(int pageNumber, int pageSize , CancellationToken ct)
        {
            return await _expsrv.GetPagedExperts(pageNumber, pageSize, ct);

        }
        public async Task<bool> DeleteExpert(CancellationToken ct, int id, int userId)
        {
            var result = await _appsrv.DeleteAppUserById(userId, ct);
            var imageDetaile= await _imagesrv.GetImageByExpertId(id, ct);
            if (result.Succeeded)
            {
                if(imageDetaile!= null)
                {
                    await fileService.Delete(imageDetaile.ImagePath, ct);
                    await _imagesrv.DeleteImage(imageDetaile.Id, ct);
                }
                await _cmtsrv.DeleteByExpertId(ct,id);
                await _sgsrv.DeleteSuggestionByExpertId(ct, id);
                return await _expsrv.DeleteExpert(ct, id);

            }
            return result.Succeeded;
           
        }
        public async Task<OutputExpertDTO?> GetExpertById(CancellationToken ct, int id)
            => await _expsrv.GetExpertById(ct, id);

        public async Task<bool> UpdateExpert(CancellationToken ct, UpdateExpertDTO updateExpert)
        {
            if (updateExpert.Password is not null)
            {
                await _appsrv.ChangePassword(updateExpert.UserId, updateExpert.Password);
            }
            await _appsrv.UpdateAppUserFields(new AppUserFieldsDTO
            {
                Id= updateExpert.UserId,
                Email=updateExpert.Email,
                FirstName=updateExpert.FirstName,
                LastName=updateExpert.LastName
             
            });
            if (updateExpert.Image is not null)
            {
                var imageDetaile = await _imagesrv.GetImageByExpertId(updateExpert.Id, ct);
               

                if (imageDetaile != null)
                {
                    await fileService.Delete(imageDetaile.ImagePath, ct);
                    string path = await fileService.Upload(updateExpert.Image, "UserProfile", ct);

                    await _imagesrv.UpdateImagePathById(path, imageDetaile.Id, ct);
                }
                else
                {
                    
                        string path = await fileService.Upload(updateExpert.Image, "UserProfile", ct);
                        var profileImageDTO = new ProfileImageDTO
                        {
                            ImagePath = path,
                            ExpertId = updateExpert.Id

                        };
                        updateExpert.ImageId = await _imagesrv.SetImageProfile(profileImageDTO, ct);
                    
                }
            }
           
            var currentHomeServices = await _expsrv.GetHomeServiceIdByExpertId(updateExpert.Id, ct);
            var newHomeService = currentHomeServices;
            if (currentHomeServices != null && currentHomeServices.Any() && updateExpert.HomeServicesId != null) 
            {
                var toAdd =updateExpert.HomeServicesId.Except(currentHomeServices).ToList();
                var toRemove = currentHomeServices.Except(updateExpert.HomeServicesId).ToList();
                if (toRemove.Any())
                {
                    newHomeService = currentHomeServices
                        .Where(hs => !toRemove.Contains(hs))
                        .ToList();
                }
                if (toAdd.Any())
                {
                     newHomeService.AddRange(toAdd);
                        
                }
           
            }
            else
            {
               
                newHomeService = updateExpert.HomeServicesId;
               
            }
            if(newHomeService !=null && newHomeService.Any())
            {
                var homeServices = await _homeService.GetHomeServicesById(newHomeService, ct);
                await _expsrv.UpdateHomeServicesForExpert(updateExpert.Id, homeServices, ct);
            }
            
                return await _expsrv.UpdateExpert(ct, updateExpert);
        }
        public async Task<IdentityResult> CreateExpertByAdmin(CreateExpertDTO expertDTO, CancellationToken ct)
        {
            var user = new AppUser
            {
                UserName = expertDTO.Email,
                Email = expertDTO.Email,
                CreatedAt = DateTime.Now,
                FirstName = expertDTO.FirstName,
                LastName = expertDTO.LastName

            };
            var result = await _userManager.CreateAsync(user, expertDTO.Password);
            if (result.Succeeded)
            {
                logger.LogInformation("کاربر جدید با ایمیل {Email} و نقش کارشناس ساخته شد.",
                    user.Email);
                expertDTO.UserId = user.Id;
                if (expertDTO.HomeServicesId is not null && expertDTO.HomeServicesId.Any())
                {
                    expertDTO.HomeServices = await _homeService.GetHomeServicesById(expertDTO.HomeServicesId, ct);
                }
               
                var expertId = await _expsrv.CreateExpertByAdmin(expertDTO, ct);
                user.ExpertId = expertId;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Expert");

                if (expertDTO.Image is not null)
                {
                    var path = await fileService.Upload(expertDTO.Image, "UserProfile", ct);
                    var profileImage = new ProfileImageDTO { ExpertId = expertId, ImagePath = path };
                    var imageId = await _imagesrv.SetImageProfile(profileImage, ct);
                    await _expsrv.UpdateImageId(expertId, imageId, ct);
                }

            }
            return result;
        }
        }

    }
