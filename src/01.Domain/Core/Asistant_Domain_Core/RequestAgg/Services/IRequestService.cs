using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.RequestAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.Services
{
    public interface IRequestService
    {
        Task<bool> ChangeRequestToAwaitingExpertArrivalOnSite(int requestId, CancellationToken ct);
        Task<int> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO);
        Task<bool> DeleteRequest(int id, CancellationToken ct);
        Task<bool> DeleteRequestByCustomerId(CancellationToken ct, int id);
        Task<bool> DeleteRequestByRequestId(int requestId, CancellationToken ct);
        Task<PagedResult<OutputRequestDTO>> GetPagedDoneRequestByCustomerId(int id, int pageNumber, int pageSize, CancellationToken ct);
        Task<PagedResult<OutputRequestDTO>> GetPagedInProgressRequestByCustomerId(int id, int pageNumber, int pageSize, CancellationToken ct);
        Task<PagedResult<OutputRequestDTO>> GetPagedRequest(int pageNumber, int pageSize, CancellationToken ct);
        Task<OutputRequestDTO?> GetRequestById(int id, CancellationToken ct);
        Task<bool> RejectRequest(int id, CancellationToken ct);
        Task<bool> UpdateCommentId(int requestId, int commentId, CancellationToken ct);
        Task<bool> UpdateVerifyExpertDate(int requestId, DateTime verifyDate, CancellationToken ct);
    }
}
