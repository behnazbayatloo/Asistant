using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.UserAgg
{
    public class ExpertRepository(ApplicationDbContext _dbcontext):IExpertRepository
    {
    }
}
