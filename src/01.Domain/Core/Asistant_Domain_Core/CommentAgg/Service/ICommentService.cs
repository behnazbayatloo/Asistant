using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.CommentAgg.Service
{
    public interface ICommentService
    {
        Task<bool> AcceptComment(int id, CancellationToken ct);
        Task<bool> DeleteByCustumerId(CancellationToken ct, int id);
        Task<bool> DeleteByExpertId(CancellationToken ct, int id);
        Task<bool> DeleteComment(int id, CancellationToken ct);
        Task<PagedResult<CommentDTO>> GetPagedComment(int pageNumber, int pageSize, CancellationToken ct);
        Task<PagedResult<CommentDTO>> GetPagedPendingComment(int pageNumber, int pageSize, CancellationToken ct);
        Task<bool> RejectComment(int id, CancellationToken ct);
    }
}
