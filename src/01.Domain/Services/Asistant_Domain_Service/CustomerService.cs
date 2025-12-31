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
    public class CustomerService(ICustomerRepository _cusrepo,ILogger<CustomerService> logger):ICustomerService
    {
    }
}
