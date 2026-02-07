using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Services
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(int userId, CancellationToken ct);
        Task<int> CreateCustomerByAdmin(CreateCustomerDTO customerDTO, CancellationToken ct);
        Task<bool> DeleteCustomer(CancellationToken ct, int id);
        Task<bool> ExistCustomer(int expertId, CancellationToken ct);
        Task<OutputCustomerDTO?> GetCustomerById(CancellationToken ct, int id);
        Task<OutputCustomerDTO?> GetCustomerByUserId(int userId, CancellationToken ct);
        Task<PagedResult<OutputCustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize, CancellationToken ct);
        Task<bool> UpdateCustomer(CancellationToken ct, UpdateCustomerDTO updateCustomerDTO);
        Task<bool> UpdateImageId(int id, int imageId, CancellationToken ct);
    }
}
