using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.Data;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.SuggestionAgg
{
    public class SuggestionRepository(ApplicationDbContext _dbcontext):ISuggestionRepository
    {
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
    }
}
