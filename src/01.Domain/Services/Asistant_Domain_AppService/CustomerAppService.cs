using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CustomerAppService(ICustomerService _cutsrv,ILogger<CustomerAppService> logger):ICustomerAppService
    {
    }
}
