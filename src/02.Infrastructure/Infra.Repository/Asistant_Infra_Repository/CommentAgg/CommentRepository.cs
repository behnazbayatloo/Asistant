using Asistant_Domain_Core.CommentAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
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
    }
}
