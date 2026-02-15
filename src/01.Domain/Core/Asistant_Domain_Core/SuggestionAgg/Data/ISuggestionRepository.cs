using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.SuggestionAgg.Data
{
    public interface ISuggestionRepository
    {
        Task<bool> AcceptSuggestion(int id, CancellationToken ct);
        Task<int> CreateSuggestion(InputSuggestionDTO inputSuggestionDTO, CancellationToken ct);
        Task<bool> DeleteSuggestionByCustomerId(CancellationToken ct, int id);
        Task<bool> DeleteSuggestionByExpertId(CancellationToken ct, int id);
        Task<OutputSuggestionDTO?> GetApproveSuggestionByRequestId(int requestId, CancellationToken ct);
        Task<PagedResult<OutputSuggestionDTO>> GetPagedSuggestionByRequestId(int id, int pageNumber, int pageSize, CancellationToken ct);
        Task<bool> RejectOtherSuggestionByRequestId(int requestId, int suggestionId, CancellationToken ct);
    }
}
