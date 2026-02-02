using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.CommentAgg.Data;
using Asistant_Domain_Core.CommentAgg.DTOs;
using Asistant_Domain_Core.CommentAgg.Enum;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.CommentAgg
{
    public class CommentRepository(ApplicationDbContext _dbcontext):ICommentRepository
    {
        public async Task<bool> DeleteByCustumerId(CancellationToken ct,int id)
        {
            return await _dbcontext.Comments.Where(c => c.CustomerId == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(c => c.IsDeleted, true), ct) > 0;
        }
        public async Task<bool> DeleteByExpertId(CancellationToken ct, int id)
        {
            return await _dbcontext.Comments.Where(c => c.ExpertId == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(c => c.IsDeleted, true), ct) > 0;
        }

        public async Task<PagedResult<CommentDTO>> GetPagedComment(int pageNumber, int pageSize,CancellationToken ct)
        {
            var query = _dbcontext.Comments
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    CustomerId = c.CustomerId,
                    CustomerName=c.Customer.User.FirstName+" "+c.Customer.User.LastName,
                    Description = c.Description,
                    ExpertId = c.ExpertId,
                    ExpertName=c.Expert.User.FirstName+" "+c.Expert.User.LastName,
                    HomeServiceId = c.HomeServiceId,
                    HomeServiceName=c.HomeService.Name,
                    Rate=c.Rate,
                    RequestId=c.RequestId,
                    Status=c.Status.ToString(),
                    Title=c.Title
                });

            return await query.ToPaginatedResult<CommentDTO>(pageNumber, pageSize, ct);

        }
        public async Task<PagedResult<CommentDTO>> GetPagedPendingComment(int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbcontext.Comments
                .Where(c=>c.Status==StatusEnum.Pending)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    CustomerId = c.CustomerId,
                    CustomerName = c.Customer.User.FirstName + " " + c.Customer.User.LastName,
                    Description = c.Description,
                    ExpertId = c.ExpertId,
                    ExpertName = c.Expert.User.FirstName + " " + c.Expert.User.LastName,
                    HomeServiceId = c.HomeServiceId,
                    HomeServiceName = c.HomeService.Name,
                    Rate = c.Rate,
                    RequestId = c.RequestId,
                    Status = c.Status.ToString(),
                    Title = c.Title
                });

            return await query.ToPaginatedResult<CommentDTO>(pageNumber, pageSize, ct);

        }
        public async Task<bool> AcceptComment(int id,CancellationToken ct)
        {
            return await _dbcontext.Comments.Where(c => c.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(c => c.Status, StatusEnum.Accept), ct)>0;
        }
        public async Task<bool> RejectComment (int id, CancellationToken ct)
        {
            return await _dbcontext.Comments.Where(c => c.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(c => c.Status, StatusEnum.Reject), ct) > 0;
        }
        public async Task<bool> DeleteComment(int id,CancellationToken ct)
        {
            return await _dbcontext.Comments.Where(c => c.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(c => c.IsDeleted, true), ct) > 0;
        }
    }
}
