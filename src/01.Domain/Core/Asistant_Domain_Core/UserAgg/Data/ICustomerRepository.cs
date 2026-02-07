using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Data
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomer(int userId, CancellationToken ct);
        Task<int> CreateCustomerByAdmin(CreateCustomerDTO customerDTO, CancellationToken ct);
        Task<bool> DeleteCustomer(CancellationToken ct, int id);
        Task<bool> ExistCustomer(int customerId, CancellationToken ct);
        Task<PagedResult<OutputCustomerDTO>> GetAllCustumers(int pageNumber, int pageSize, CancellationToken ct);
        Task<OutputCustomerDTO?> GetCustomerById(CancellationToken ct, int id);
        Task<OutputCustomerDTO?> GetCustomerByUserId(int userId, CancellationToken ct);
        Task<bool> UpdateCustomer(CancellationToken ct, UpdateCustomerDTO updatedCustomer);
        Task<bool> UpdateImageId(int id, int imageId, CancellationToken ct);
    }
}
