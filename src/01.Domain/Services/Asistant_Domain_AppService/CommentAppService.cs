using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_Domain_Core.CommentAgg.DTOs;
using Asistant_Domain_Core.CommentAgg.Service;
using Asistant_Domain_Core.RequestAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CommentAppService(ICommentService _cmtserv,IRequestService _requestService,ILogger<CommentAppService> logger):ICommentAppService
    {
        public async Task<PagedResult<CommentDTO>> GetPagedComment(int pageNumber, int pageSize, CancellationToken ct, bool? showAll = null)
        {
            if(showAll==true || showAll==null)
            {
                return await _cmtserv.GetPagedComment(pageNumber, pageSize, ct);
            }
            return await _cmtserv.GetPagedPendingComment(pageNumber, pageSize, ct);

        }
        public async Task<bool> AcceptComment(int id, CancellationToken ct)
            => await _cmtserv.AcceptComment(id, ct);
        public async Task<bool> RejectComment(int id, CancellationToken ct)
             => await _cmtserv.RejectComment(id, ct);
        public async Task<bool> DeleteComment(int id,int requestId ,CancellationToken ct)
        {
           var deleted = await _cmtserv.DeleteComment(id, ct);
            if (deleted)
            {
            return  await _requestService.UpdateCommentId(requestId, 0, ct);
                
            }
            return deleted;
        }
        public async Task<bool> CreateComment(InputCommentDTO commentDTO, CancellationToken ct)
        {
           var commentId= await _cmtserv.CreateComment(commentDTO, ct);
            if (commentId > 0)
            {
                return await _requestService.UpdateCommentId(commentDTO.RequestId, commentId, ct);
            }
            else
                return false;
            
        }
        public async Task<CommentDTO?> GetCommentByRequestId(int requestId, CancellationToken ct)
            => await _cmtserv.GetCommentByRequestId(requestId, ct);
        public async Task<PagedResult<CommentDTO>> GetPagedCommentByCustomerId(int customerId, int pageNumber, int pageSize, CancellationToken ct)
           => await _cmtserv.GetPagedCommentByCustomerId(customerId, pageNumber, pageSize, ct);
    }
}
