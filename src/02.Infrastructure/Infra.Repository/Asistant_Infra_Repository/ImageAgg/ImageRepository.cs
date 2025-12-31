using Asistant_Domain_Core.ImageAgg.Data;
using Asistant_Infra_Db_Sql.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.ImageAgg
{
    public class ImageRepository(ApplicationDbContext _dbcontext):IImageRepository
    {
    }
}
