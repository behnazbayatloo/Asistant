using Asistant_Domain_Core.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Data
{
    public interface ICityDapperRepository
    {
       Task<List<City>> GetAllCities(CancellationToken ct);
    }
}
