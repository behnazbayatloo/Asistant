using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.Enums;
using Asistant_Domain_Core.SuggestionAgg.Data;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.Enums;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asistant_Domain_Core.SuggestionAgg.Entity;

namespace Asistant_Infra_Repository.SuggestionAgg
{
    public class SuggestionRepository(ApplicationDbContext _dbcontext):ISuggestionRepository
    {
        public async Task<int> CreateSuggestion(InputSuggestionDTO inputSuggestionDTO,CancellationToken ct)
        {
            var suggestion = new Suggestion
            {
                CreatedAt = inputSuggestionDTO.CreatedAt,
                Description = inputSuggestionDTO.Description,
                ExpertId= inputSuggestionDTO.ExpertId,
                Price= inputSuggestionDTO.Price,
                Status=StatusEnum.Pending,
                Title= inputSuggestionDTO.Title,
                RequestId=inputSuggestionDTO.RequestId,
               HomeServiceId=inputSuggestionDTO.HomeServiceId
                
            };
            await _dbcontext.Suggestions.AddAsync(suggestion,ct);
             await _dbcontext.SaveChangesAsync(ct);
            return suggestion.Id;
        }
        public async Task<bool> DeleteSuggestionByExpertId(CancellationToken ct, int id)
        {
            return await _dbcontext.Suggestions.Where(s => s.ExpertId == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(s => s.IsDeleted, true), ct) > 0;
        }
        public async Task<bool> DeleteSuggestionByCustomerId(CancellationToken  ct, int id)
        {
            return await _dbcontext.Suggestions.Where(s => s.Request.CustomerId == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(s => s.IsDeleted, true), ct) > 0;
        }
        public async Task<PagedResult<OutputSuggestionDTO>> GetPagedSuggestionByRequestId(int id,int pageNumber,int pageSize,CancellationToken ct)
        {
            var query = _dbcontext.Suggestions
                           .AsNoTracking()
                           .OrderBy(c => c.Id)
                           .Where(s=>s.RequestId==id)
                           .Select(c => new OutputSuggestionDTO
                           {
                               Id = c.Id,
                               Title = c.Title,
                               CreatedAt = c.CreatedAt,
                               Description = c.Description,
                               ExpertId = c.ExpertId,
                               ExpertName = c.Expert.User.FirstName + " " + c.Expert.User.LastName,
                               Price = c.Price,
                               ImagesId = c.Images != null ? c.Images.Select(i => i.Id).ToList() : new List<int>(),
                               RequestId = c.RequestId,
                               Status = c.Status.ToString()


                           });

            return await query.ToPaginatedResult<OutputSuggestionDTO>(pageNumber, pageSize, ct);
        }
        public async Task<OutputSuggestionDTO?> GetApproveSuggestionByRequestId(int requestId, CancellationToken ct)
        {
            return await _dbcontext.Suggestions
                .AsNoTracking()
                .Where(s => s.RequestId == requestId && s.Status == StatusEnum.Accept)
                .Select(s => new OutputSuggestionDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    CreatedAt = s.CreatedAt,
                    Description = s.Description,
                    ExpertId = s.ExpertId,
                    ExpertName = s.Expert.User.FirstName + " " + s.Expert.User.LastName,
                    Price = s.Price,
                    ImagesId = s.Images != null ? s.Images.Select(i => i.Id).ToList() : new List<int>(),
                    RequestId = s.RequestId,
                    Status = s.Status.ToString()

                }).FirstOrDefaultAsync(ct);
        }
        public async Task<bool> RejectOtherSuggestionByRequestId(int requestId,int suggestionId ,CancellationToken ct)
        {
            return await _dbcontext.Suggestions
                .Where(s=>s.RequestId==requestId && s.Id!=suggestionId)
                .ExecuteUpdateAsync(set=>set.SetProperty(s=>s.Status, StatusEnum.Reject),ct)>0;
        }
        public async Task<bool> AcceptSuggestion(int id,CancellationToken ct)
        {
            return await _dbcontext.Suggestions.Where(s=>s.Id==id)
                .ExecuteUpdateAsync(set=>set.SetProperty(s=>s.Status,StatusEnum.Accept),ct)>0;
        }
    }
}
