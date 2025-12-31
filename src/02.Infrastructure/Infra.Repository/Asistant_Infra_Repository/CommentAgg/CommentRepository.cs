using Asistant_Domain_Core.CommentAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.CommentAgg
{
    public class CommentRepository(ApplicationDbContext _dbcontext):ICommentRepository
    {
    }
}
