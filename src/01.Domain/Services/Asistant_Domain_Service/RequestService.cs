using Asistant_Domain_Core.RequestAgg.Data;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.RequestAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class RequestService (IRequestRepository _reqrepo,ILogger<RequestService> logger):IRequestService
    {
        public async Task<bool> DeleteRequestByCustomerId(CancellationToken ct, int id)
            => await _reqrepo.DeleteRequestByCustomerId(ct, id);
        public async Task<int> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO)
            => await _reqrepo.CreateRequest(ct, requestDTO);
    }
}
