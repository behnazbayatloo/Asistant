using Asistant_Domain_Core.SuggestionAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
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
    }
}
