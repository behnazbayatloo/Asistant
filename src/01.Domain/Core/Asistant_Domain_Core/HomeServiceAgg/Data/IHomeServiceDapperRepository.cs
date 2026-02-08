using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.Data
{
    public interface IHomeServiceDapperRepository
    {
        Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct);
    }
}
