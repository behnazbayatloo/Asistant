using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.SuggestionAgg.Services
{
    public interface ISuggestionService
    {
        Task<bool> DeleteSuggestionByCustomerId(CancellationToken ct, int id);
        Task<bool> DeleteSuggestionByExpertId(CancellationToken ct, int id);
    }
}
