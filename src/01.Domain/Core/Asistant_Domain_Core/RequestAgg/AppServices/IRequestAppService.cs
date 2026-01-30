using Asistant_Domain_Core.RequestAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.AppServices
{
    public interface IRequestAppService
    {
        Task<bool> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO);
    }
}
