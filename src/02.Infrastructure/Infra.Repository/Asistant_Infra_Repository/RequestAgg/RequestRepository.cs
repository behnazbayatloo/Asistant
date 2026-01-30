using Asistant_Domain_Core.RequestAgg.Data;
using Asistant_Domain_Core.RequestAgg.DTOs;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Enums;
using Asistant_Infra_Db_Sql.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.RequestAgg
{
    public class RequestRepository(ApplicationDbContext _dbcontext):IRequestRepository
    {
        public async Task<bool> DeleteRequestByCustomerId(CancellationToken ct,int id)
        {
            return await _dbcontext.Requests.Where(r => r.CustomerId == id)
                .ExecuteUpdateAsync(setter => setter.SetProperty(r => r.IsDeleted, true), ct) > 0;
        }
        public async Task<int> CreateRequest(CancellationToken ct, InputRequestDTO requestDTO)
        {
            var request = new Request
            {
                Title = requestDTO.Title,
                AppointmentReadyDate = requestDTO.AppointmentReadyDate,
                CreatedAt = requestDTO.CreatedAt,
                HomeServiceId = requestDTO.HomeServiceId,
                CustomerId = requestDTO.CustomerId,
                Description = requestDTO.Description,
                Status = StatusEnum.PendingExpertApproval,

            };
            await _dbcontext.Requests.AddAsync(request, ct);
            await _dbcontext.SaveChangesAsync(ct);
            return request.Id;
        
        }
    }
}
