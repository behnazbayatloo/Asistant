using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_Domain_Core.CommentAgg.DTOs;
using Asistant_Domain_Core.CommentAgg.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CommentAppService(ICommentService _cmtserv,ILogger<CommentAppService> logger):ICommentAppService
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
        public async Task<bool> DeleteComment(int id, CancellationToken ct)
            => await _cmtserv.DeleteComment(id, ct);
    }
}
