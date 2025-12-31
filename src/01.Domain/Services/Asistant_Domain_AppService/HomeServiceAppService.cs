using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class HomeServiceAppService(IHomeServiceService _hmsrv,ILogger<HomeServiceAppService> logger):IHomeServiceAppService
    {
    }
}
