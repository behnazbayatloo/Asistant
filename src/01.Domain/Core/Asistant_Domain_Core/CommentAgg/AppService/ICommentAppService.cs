using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.CommentAgg.AppService
{
    public interface ICommentAppService
    {
        Task<bool> AcceptComment(int id, CancellationToken ct);
        Task<Result<bool>> CreateComment(InputCommentDTO commentDTO, CancellationToken ct);
        Task<bool> DeleteComment(int id,int requestId, CancellationToken ct);
        Task<CommentDTO?> GetCommentByRequestId(int requestId, CancellationToken ct);
        Task<PagedResult<CommentDTO>> GetPagedComment(int pageNumber, int pageSize, CancellationToken ct, bool? showAll = null);
        Task<PagedResult<CommentDTO>> GetPagedCommentByCustomerId(int customerId, int pageNumber, int pageSize, CancellationToken ct);
        Task<PagedResult<CommentDTO>> GetPagedCommentByExpertId(int expertId, int homeServiceId, int pageNumber, int pageSize, CancellationToken ct);
        Task<bool> RejectComment(int id, CancellationToken ct);
    }
}
