using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.RequestAgg.Data;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Enums;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.RequestAgg
{
    public class RequestRepository(ApplicationDbContext _dbcontext):IRequestRepository
    {
        public async Task<bool> DeleteRequestByCustomerId(CancellationToken ct,int id)
        {
            return await _dbcontext.Requests.Where(r => r.CustomerId == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(r => r.IsDeleted, true), ct) > 0;
        }
        public async Task<int> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO)
        {
            var request = new Request
            {
                Title = requestDTO.Title,
                AppointmentReadyDate = requestDTO.AppointmentReadyDate,
                CreatedAt = requestDTO.CreatedAt,
                HomeServiceId = requestDTO.HomeServiceId,
                CustomerId = requestDTO.CustomerId,
                Description = requestDTO.Description,
                Status = StatusEnum.PendingExpertApproval,

            };
            await _dbcontext.Requests.AddAsync(request, ct);
            await _dbcontext.SaveChangesAsync(ct);
            return request.Id;
        
        }
        public async Task<PagedResult<OutputRequestDTO>> GetPagedRequest(int pageNumber,int pageSize,CancellationToken ct)
        {
            var query = _dbcontext.Requests
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new OutputRequestDTO
                {
                    Id= c.Id,
                    Title = c.Title,
                    AppointmentReadyDate = c.AppointmentReadyDate,
                    CommentId= c.CommentId,
                    CompletedDate = c.CompletedDate,
                    CreatedAt= c.CreatedAt,
                    CustomerId=c.CustomerId,
                    CustomerName=c.Customer.User.FirstName+" "+c.Customer.User.LastName,
                    Description=c.Description,
                    HomeServiceId=c.HomeServiceId,
                    HomeServiceName=c.HomeService.Name,
                    Status=c.Status.ToString(),
                    VerifyExpertDate=c.VerifyExpertDate,
                    SuggestionsId= c.Suggestions != null  ? c.Suggestions.Select(s => s.Id).ToList() : new List<int>(),
                    SuggesstionCount= c.Suggestions!= null ? c.Suggestions.Count : 0,
                    ImagesId= c.Images != null  ? c.Images.Select(i=>i.Id).ToList() : new List<int>()


                });

            return await query.ToPaginatedResult<OutputRequestDTO>(pageNumber, pageSize, ct);

        }
        public async Task<bool> RejectRequest(int id, CancellationToken ct)
        {
            return await _dbcontext.Requests.Where(r => r.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(r => r.Status, StatusEnum.RejectedByAdmin), ct) > 0;
        }
        public async Task<bool> DeleteRequest(int id,CancellationToken ct)
        {
            return await _dbcontext.Requests.Where(r=>r.Id==id)
                .ExecuteUpdateAsync(set=>set.SetProperty(r=>r.IsDeleted,true), ct) > 0;
        }
        public async Task<OutputRequestDTO?> GetRequestById(int id, CancellationToken ct)
        {
            return await _dbcontext.Requests
                .Where(r=>r.Id==id)
                .Select(r=> new OutputRequestDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    AppointmentReadyDate = r.AppointmentReadyDate,
                    CommentId = r.CommentId,
                    CompletedDate = r.CompletedDate,
                    CreatedAt = r.CreatedAt,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.User.FirstName + " " + r.Customer.User.LastName,
                    Description = r.Description,
                    HomeServiceId = r.HomeServiceId,
                    HomeServiceName = r.HomeService.Name,
                    Status = r.Status.ToString(),
                    VerifyExpertDate = r.VerifyExpertDate,
                    SuggestionsId = r.Suggestions != null ? r.Suggestions.Select(s => s.Id).ToList() : new List<int>(),
                    SuggesstionCount = r.Suggestions != null ? r.Suggestions.Count : 0,
                    ImagesId = r.Images != null ? r.Images.Select(i => i.Id).ToList() : new List<int>()

                }).FirstOrDefaultAsync(ct);
        }

        public async Task<PagedResult<OutputRequestDTO>> GetPagedDoneRequestByCustomerId(int id,int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbcontext.Requests
                .AsNoTracking()
                .Where(r => r.CustomerId == id && ( r.Status == StatusEnum.Completed || r.Status==StatusEnum.RejectedByAdmin))
              
                .OrderBy(c => c.Id)
                
                .Select(c => new OutputRequestDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    AppointmentReadyDate = c.AppointmentReadyDate,
                    CommentId = c.CommentId,
                    CompletedDate = c.CompletedDate,
                    CreatedAt = c.CreatedAt,
                    CustomerId = c.CustomerId,
                    CustomerName = c.Customer.User.FirstName + " " + c.Customer.User.LastName,
                    Description = c.Description,
                    HomeServiceId = c.HomeServiceId,
                    HomeServiceName = c.HomeService.Name,
                    Status = c.Status.ToString(),
                    VerifyExpertDate = c.VerifyExpertDate,
                    SuggestionsId = c.Suggestions != null ? c.Suggestions.Select(s => s.Id).ToList() : new List<int>(),
                    SuggesstionCount = c.Suggestions != null ? c.Suggestions.Count : 0,
                    ImagesId = c.Images != null ? c.Images.Select(i => i.Id).ToList() : new List<int>()


                });

            return await query.ToPaginatedResult<OutputRequestDTO>(pageNumber, pageSize, ct);

        }
        public async Task<PagedResult<OutputRequestDTO>> GetPagedInProgressRequestByCustomerId(int id,int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbcontext.Requests
                .AsNoTracking()
                  .Where(r => r.CustomerId == id &&
                  (r.Status != StatusEnum.InProgress || r.Status!= StatusEnum.AwaitingExpertArrivalOnSite 
                  || r.Status==StatusEnum.PendingSuggestionApproval
                  || r.Status == StatusEnum.PendingExpertApproval
                 ))
                .OrderBy(c => c.Id)
                .Select(c => new OutputRequestDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    AppointmentReadyDate = c.AppointmentReadyDate,
                    CommentId = c.CommentId,
                    CompletedDate = c.CompletedDate,
                    CreatedAt = c.CreatedAt,
                    CustomerId = c.CustomerId,
                    CustomerName = c.Customer.User.FirstName + " " + c.Customer.User.LastName,
                    Description = c.Description,
                    HomeServiceId = c.HomeServiceId,
                    HomeServiceName = c.HomeService.Name,
                    Status = c.Status.ToString(),
                    VerifyExpertDate = c.VerifyExpertDate,
                    SuggestionsId = c.Suggestions != null ? c.Suggestions.Select(s => s.Id).ToList() : new List<int>(),
                    SuggesstionCount = c.Suggestions != null ? c.Suggestions.Count : 0,
                    ImagesId = c.Images != null ? c.Images.Select(i => i.Id).ToList() : new List<int>()


                });

            return await query.ToPaginatedResult<OutputRequestDTO>(pageNumber, pageSize, ct);

        }
        public async Task<bool> DeleteRequestByRequestId(int requestId,CancellationToken ct)
        {
            return await _dbcontext.Requests
                .Where(r => r.Id == requestId &&
                (r.Status == StatusEnum.PendingSuggestionApproval || r.Status == StatusEnum.PendingExpertApproval))
                .ExecuteUpdateAsync(set => set.SetProperty(r => r.IsDeleted, true), ct) > 0;
        }
        public async Task<bool> ChangeRequestToAwaitingExpertArrivalOnSite(int requestId,CancellationToken ct)
        {
            return await _dbcontext.Requests.Where(r => r.Id == requestId)
                .ExecuteUpdateAsync(set => 
                set.SetProperty(r => r.Status, StatusEnum.AwaitingExpertArrivalOnSite), ct) > 0;
        }
    }
}
