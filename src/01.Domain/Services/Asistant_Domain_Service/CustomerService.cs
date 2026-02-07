using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class CustomerService(ICustomerRepository _cusrepo,ILogger<CustomerService> logger):ICustomerService
    {
        public async Task<PagedResult<OutputCustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize, CancellationToken ct)
        {
            if (pageSize <= 0) pageSize = 10;
            return await _cusrepo.GetAllCustumers(pageNumber, pageSize, ct);
        }
        public async Task<bool> DeleteCustomer(CancellationToken ct, int id) 
            => await _cusrepo.DeleteCustomer(ct, id);
        public async Task<OutputCustomerDTO?> GetCustomerById(CancellationToken ct, int id)
            => await _cusrepo.GetCustomerById(ct, id);
        public async Task<bool> UpdateCustomer(CancellationToken ct, UpdateCustomerDTO updateCustomerDTO)
            => await _cusrepo.UpdateCustomer(ct, updateCustomerDTO);
        public async Task<int> CreateCustomer(int userId,CancellationToken ct)
            =>await _cusrepo.CreateCustomer(userId,ct);
        public async Task<int> CreateCustomerByAdmin(CreateCustomerDTO customerDTO,CancellationToken ct)
            => await _cusrepo.CreateCustomerByAdmin(customerDTO,ct);    
        public async Task<bool> UpdateImageId(int id,int imageId,CancellationToken ct)
            => await _cusrepo.UpdateImageId(id,imageId,ct);
        public async Task<OutputCustomerDTO?> GetCustomerByUserId(int userId, CancellationToken ct)
            => await _cusrepo.GetCustomerByUserId(userId, ct);
        public async Task<bool> ExistCustomer(int expertId, CancellationToken ct)
            => await _cusrepo.ExistCustomer(expertId, ct);
    }
}
