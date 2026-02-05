using Asistant_Domain_Core._commonEntities;
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
        public async Task<PagedResult<OutputRequestDTO>> GetPagedRequest(int pageNumber, int pageSize, CancellationToken ct)
   => await _reqrepo.GetPagedRequest(pageNumber, pageSize, ct);
        public async Task<bool> RejectRequest(int id, CancellationToken ct)
       => await _reqrepo.RejectRequest(id, ct);
        public async Task<bool> DeleteRequest(int id, CancellationToken ct)
            => await _reqrepo.DeleteRequest(id,ct);
        public async Task<OutputRequestDTO?> GetRequestById(int id, CancellationToken ct)
            => await _reqrepo.GetRequestById(id, ct);
        public async Task<PagedResult<OutputRequestDTO>> GetPagedInProgressRequestByCustomerId(int id, int pageNumber, int pageSize, CancellationToken ct)
            => await _reqrepo.GetPagedInProgressRequestByCustomerId(id, pageNumber, pageSize, ct);
        public async Task<PagedResult<OutputRequestDTO>> GetPagedDoneRequestByCustomerId(int id, int pageNumber, int pageSize, CancellationToken ct)
            => await _reqrepo.GetPagedDoneRequestByCustomerId(id,pageNumber, pageSize, ct);
        public async Task<bool> DeleteRequestByRequestId(int requestId, CancellationToken ct)
            => await _reqrepo.DeleteRequestByRequestId(requestId, ct);
        public async Task<bool> ChangeRequestToAwaitingExpertArrivalOnSite(int requestId, CancellationToken ct)
            => await _reqrepo.ChangeRequestToAwaitingExpertArrivalOnSite(requestId, ct);
            }
}
