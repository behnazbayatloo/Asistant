using Asistant_Domain_Core.SuggestionAgg.Data;
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
    }
}
