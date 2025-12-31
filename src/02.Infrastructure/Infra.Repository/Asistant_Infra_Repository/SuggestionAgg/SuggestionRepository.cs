using Asistant_Domain_Core.SuggestionAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.SuggestionAgg
{
    public class SuggestionRepository(ApplicationDbContext _dbcontext):ISuggestionRepository
    {
    }
}
