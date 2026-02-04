using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.Data;
using Asistant_Domain_Core.CommentAgg.DTOs;
using Asistant_Domain_Core.CommentAgg.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class CommentService(ICommentRepository _cmtrepo,ILogger<CommentService> logger):ICommentService
    {
        public async Task<bool> DeleteByCustumerId(CancellationToken ct, int id)
            => await _cmtrepo.DeleteByCustumerId(ct, id);
        
        public async Task<bool> DeleteByExpertId(CancellationToken ct, int id)
            => await _cmtrepo.DeleteByExpertId(ct,id);

        public async Task<PagedResult<CommentDTO>> GetPagedComment(int pageNumber, int pageSize, CancellationToken ct)
            => await _cmtrepo.GetPagedComment(pageNumber, pageSize, ct);
        public async Task<PagedResult<CommentDTO>> GetPagedPendingComment(int pageNumber, int pageSize, CancellationToken ct)
            => await _cmtrepo.GetPagedPendingComment(pageNumber, pageSize, ct);
        public async Task<bool> AcceptComment(int id, CancellationToken ct)
            => await _cmtrepo.AcceptComment(id, ct);
        public async Task<bool> RejectComment(int id, CancellationToken ct)
             => await _cmtrepo.RejectComment(id, ct);
        public async Task<bool> DeleteComment(int id, CancellationToken ct)
            => await _cmtrepo.DeleteComment(id, ct);
        public async Task<bool> CreateComment(InputCommentDTO commentDTO, CancellationToken ct)
            => await _cmtrepo.CreateComment(commentDTO, ct);
        public async Task<CommentDTO?> GetCommentByRequestId(int requestId, CancellationToken ct)
            => await _cmtrepo.GetCommentByRequestId(requestId, ct);
    }
}
