using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_Domain_Core.CommentAgg.Service;
using Asistant_Domain_Core.ImageAgg.DTOs;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Domain_Core.RequestAgg.Services;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
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
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CustomerAppService(UserManager<AppUser> _userManager,
        SignInManager<AppUser> _signInManager,
        RoleManager<IdentityRole<int>> _roleManager, IAppUserService _appsrv,
        ICommentService _cmtsrv,IRequestService _rqsrv,ISuggestionService _sgsrv,
         IFileService fileService,IImageService _imageService,
        ICustomerService _cutsrv,ILogger<CustomerAppService> logger):ICustomerAppService
    {
        public async Task<PagedResult<OutputCustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize, CancellationToken ct)
        {
            return await _cutsrv.GetPagedCustomers(pageNumber, pageSize, ct);
        }
        public async Task<bool> DeleteCustomer(CancellationToken ct, int id, int userId)
        {
            var result =await _appsrv.DeleteAppUserById(userId, ct);
            var imageDetaile = await _imageService.GetImageByCustomerId(id, ct);
            if (result.Succeeded)
            {
                if(imageDetaile != null)
                {
                    await fileService.Delete(imageDetaile.ImagePath, ct);
                    await _imageService.DeleteImage(imageDetaile.Id, ct);
                }
              
                await _cmtsrv.DeleteByCustumerId(ct, id);
                await _rqsrv.DeleteRequestByCustomerId(ct, id);
                await _sgsrv.DeleteSuggestionByCustomerId(ct, id);

               return await _cutsrv.DeleteCustomer(ct, id);
                
            }
            return result.Succeeded;
        }
        public async Task<OutputCustomerDTO?> GetCustomerById(CancellationToken ct, int id)
            => await _cutsrv.GetCustomerById(ct, id);

        public async Task<bool> UpdateCustomer(CancellationToken ct,UpdateCustomerDTO updateCustomerDTO)
        {
            if (updateCustomerDTO.Password is not null)
            {
                await _appsrv.ChangePassword(updateCustomerDTO.UserId,updateCustomerDTO.Password);
            }
            await _appsrv.UpdateAppUserFields(new AppUserFieldsDTO
            {
                Email = updateCustomerDTO.Email,
                FirstName = updateCustomerDTO.FirstName,
                Id = updateCustomerDTO.UserId,
                LastName = updateCustomerDTO.LastName,
                Ballance=updateCustomerDTO.Balance
            });
            if (updateCustomerDTO.Image is not null)
            {
                var imageDetaile = await _imageService.GetImageByCustomerId(updateCustomerDTO.Id, ct);
                if (imageDetaile != null)
                {
                    await fileService.Delete(imageDetaile.ImagePath, ct);
                    string path = await fileService.Upload(updateCustomerDTO.Image, "UserProfile", ct);

                    await _imageService.UpdateImagePathById(path, imageDetaile.Id, ct);
                }

                else
                {
                    string path = await fileService.Upload(updateCustomerDTO.Image, "UserProfile", ct);
                    var profileImageDTO = new ProfileImageDTO
                    {
                        ImagePath = path,
                        CustomerId = updateCustomerDTO.Id

                    };
                    updateCustomerDTO.ImageId = await _imageService.SetImageProfile(profileImageDTO, ct);
                }
            }
               

                return await _cutsrv.UpdateCustomer(ct, updateCustomerDTO);
        }
        public async Task<IdentityResult> CreateCustomerByAdmin(CreateCustomerDTO customerDTO, CancellationToken ct)
        {
            var user = new AppUser
            {
                UserName = customerDTO.Email,
                Email = customerDTO.Email,
                CreatedAt = DateTime.Now,
                FirstName=customerDTO.FirstName,
                LastName=customerDTO.LastName,
                Balance=customerDTO.Balance,

            };
            var result = await _userManager.CreateAsync(user, customerDTO.Password);
            if (result.Succeeded)
            {
                logger.LogInformation("کاربر جدید با ایمیل {Email} و نقش مشتری ساخته شد.",
                    user.Email);


                customerDTO.UserId= user.Id;
                var customerId=await _cutsrv.CreateCustomerByAdmin(customerDTO, ct);
                user.CustomerId= customerId;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Customer");
                
                if (customerDTO.Image is not null)
                {
                    var path =await fileService.Upload(customerDTO.Image, "UserProfile", ct);
                    var profileImage = new ProfileImageDTO {CustomerId=customerId,ImagePath=path };
                    var imageId =await _imageService.SetImageProfile(profileImage, ct);
                    await _cutsrv.UpdateImageId(customerId, imageId,ct);
                }
                
            }
            return result;
        }
        public async Task<OutputCustomerDTO?> GetCustomerByUserId(int userId, CancellationToken ct)
            => await _cutsrv.GetCustomerByUserId(userId, ct);
        public async Task<Result<bool>> ApproveSuggestion(ApproveSuggestionDTO approveSuggestion,CancellationToken ct)
        {
            var result = await Transaction(approveSuggestion.CustomerId,
                approveSuggestion.ExpertId, approveSuggestion.Price, ct);
            if(result.IsSuccess)
            {
                await _sgsrv.RejectOtherSuggestionByRequestId(approveSuggestion.RequestId, approveSuggestion.SuggestionId, ct);


                await _sgsrv.AcceptSuggestion(approveSuggestion.SuggestionId, ct);
                await _rqsrv.ChangeRequestToAwaitingExpertArrivalOnSite(approveSuggestion.RequestId, ct);
                return result;
            }
            return result;
            
        }


        private async Task<Result<bool>> Transaction(int customerId,int expertId,decimal price, CancellationToken ct)
        {
            
              var customerBalance = await _appsrv.GetBallanceByCustomerId(customerId, ct);
                if (customerBalance == null || customerBalance < price)
                {

                return Result<bool>.Failure("موجودی حساب شما کافی نیست");
                }
                 var expertBalance = await _appsrv.GetBallanceByExpertId(expertId, ct);
                if (expertBalance == null)
                {
                return Result<bool>.Failure("کارشناس وجود ندارد");

                }
                var updatedCustomer = await _appsrv.UpdateBallanceForCustomer(customerId, customerBalance.Value - price, ct); 
                if (!updatedCustomer)
                { 
                   
                    return Result<bool>.Failure("عملیات با مشکل مواجه شد!");
                  }
             
                var updatedExpert = await _appsrv.UpdateBallanceForExpert(expertId, expertBalance.Value + price, ct); 
                if (!updatedExpert)
                {
                await _appsrv.UpdateBallanceForCustomer(customerId, customerBalance.Value, ct);
                return Result<bool>.Failure("عملیات با مشکل مواجه شد!");

                  }

            return Result<bool>.Success(updatedExpert, $" از حساب شما کسر گردید{price}مبلغ ");

            }

    }

}
