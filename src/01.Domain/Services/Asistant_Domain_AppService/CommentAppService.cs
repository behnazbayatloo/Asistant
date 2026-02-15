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
        public async Task<Result<bool>> CreateComment(InputCommentDTO commentDTO, CancellationToken ct)
        {
            var request = await _requestService.IsRequestForCustomer(commentDTO.CustomerId, commentDTO.RequestId, ct);
            if(!request)
            {
                return Result<bool>.Failure("این درخواست برای شما نیست");
            }
           var commentId= await _cmtserv.CreateComment(commentDTO, ct);
            if (commentId > 0)
            {
                var result = await _requestService.UpdateCommentId(commentDTO.RequestId, commentId, ct);
                return Result<bool>.Success(result, "کامنت با موفقیت ثبت گردید");
            }
            else
                return Result<bool>.Failure("کامنت ثبت نشد");
            
        }
        public async Task<CommentDTO?> GetCommentByRequestId(int requestId, CancellationToken ct)
            => await _cmtserv.GetCommentByRequestId(requestId, ct);
        public async Task<PagedResult<CommentDTO>> GetPagedCommentByCustomerId(int customerId, int pageNumber, int pageSize, CancellationToken ct)
           => await _cmtserv.GetPagedCommentByCustomerId(customerId, pageNumber, pageSize, ct);
        public async Task<PagedResult<CommentDTO>> GetPagedCommentByExpertId(int expertId, int homeServiceId, int pageNumber, int pageSize, CancellationToken ct)
          => await _cmtserv.GetPagedCommentByExpertId(expertId, homeServiceId, pageNumber, pageSize, ct);
    }
}
