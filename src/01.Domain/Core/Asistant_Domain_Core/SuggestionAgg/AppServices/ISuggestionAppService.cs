using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.SuggestionAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.SuggestionAgg.AppServices
{
    public interface ISuggestionAppService
    {
        Task<PagedResult<OutputSuggestionDTO>> GetPagedSuggestionByRequestId(int id, int pageNumber, int pageSize, CancellationToken ct);
    }
}
