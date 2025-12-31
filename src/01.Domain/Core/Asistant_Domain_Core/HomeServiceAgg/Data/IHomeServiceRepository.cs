using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.Data
{
    public interface IHomeServiceRepository
    {
        Task<int> CreateHomeService(InputHomeServiceDTO homeServiceDto);
        Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct);
        Task<IEnumerable<GetHomeServiceDTO>> GetHomeServicesByCategoryId(int categoryId, CancellationToken ct);
        Task<bool> UpdateBasePrice(int id, decimal newPrice, CancellationToken ct);
        Task<bool> UpdateImagePath(int id, string imagePath, CancellationToken ct);
    }
}
