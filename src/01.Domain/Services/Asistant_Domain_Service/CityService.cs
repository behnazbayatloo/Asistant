using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class CityService(ICityRepository _ctyrepo,ILogger<CityService>  logger):ICityService
    {
    }
}
