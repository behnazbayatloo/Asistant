using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.SuggestionAgg.Data;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using Asistant_Domain_Core.SuggestionAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class SuggestionService(ISuggestionRepository _sugrepo,ILogger<SuggestionService> logger):ISuggestionService
    {
        public async Task<bool> DeleteSuggestionByExpertId(CancellationToken ct, int id)
      => await _sugrepo.DeleteSuggestionByExpertId(ct, id);
        public async Task<bool> DeleteSuggestionByCustomerId(CancellationToken ct, int id)
       => await _sugrepo.DeleteSuggestionByCustomerId(ct, id);
        public async Task<PagedResult<OutputSuggestionDTO>> GetPagedSuggestionByRequestId(int id, int pageNumber, int pageSize, CancellationToken ct)
=> await _sugrepo.GetPagedSuggestionByRequestId(id, pageNumber, pageSize, ct);
        public async Task<OutputSuggestionDTO?> GetApproveSuggestionByRequestId(int requestId, CancellationToken ct)
            => await _sugrepo.GetApproveSuggestionByRequestId(requestId, ct);
        public async Task<bool> RejectOtherSuggestionByRequestId(int requestId, int suggestionId, CancellationToken ct)
       => await _sugrepo.RejectOtherSuggestionByRequestId(requestId, suggestionId, ct); 
        public async Task<bool> AcceptSuggestion(int id, CancellationToken ct)
            => await _sugrepo.AcceptSuggestion(id, ct);
    }
}
