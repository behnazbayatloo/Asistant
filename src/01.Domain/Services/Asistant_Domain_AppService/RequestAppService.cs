using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.RequestAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class RequestAppService(IRequestService _rqsrv,ILogger<RequestAppService> logger):IRequestAppService
    {
    }
}
