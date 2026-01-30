using Asistant_Domain_Core.RequestAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.Services
{
    public interface IRequestService
    {
        Task<int> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO);
        Task<bool> DeleteRequestByCustomerId(CancellationToken ct, int id);
    }
}
