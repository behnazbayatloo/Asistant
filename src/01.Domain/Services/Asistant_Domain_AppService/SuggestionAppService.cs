using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class SuggestionAppService(ISuggestionService _sugsrv,ILogger<SuggestionAppService> logger):ISuggestionAppService
    {
    }
}
