using Asistant_Domain_Core.RequestAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.RequestAgg
{
    public class RequestRepository(ApplicationDbContext _dbcontext):IRequestRepository
    {
    }
}
