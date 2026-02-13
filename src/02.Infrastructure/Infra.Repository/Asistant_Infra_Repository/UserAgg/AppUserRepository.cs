using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Repository.UserAgg
{
    public class AppUserRepository(ApplicationDbContext _dbcontext) : IAppUserRepository
    {
        public async Task<PagedResult<AdminDTO>> GetAdminsPagedResults(int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbcontext.Users
                . Where(u=>_dbcontext.UserRoles
                .Any(ur=>ur.UserId==u.Id && ur.RoleId==1))
                 .AsNoTracking()
                 .OrderBy(a => a.Id)

                 .Select(a => new AdminDTO

                 {
                     Id = a.Id,
                    
                     Email = a.Email,
                     FirstName = a.FirstName,
                     LastName = a.LastName,
                    Balance = a.Balance
                 });

            return await query.ToPaginatedResult<AdminDTO>(pageNumber, pageSize, ct);

        }
        public async Task<decimal?> GetBallanceByCustomerId(int customerId, CancellationToken ct)
        {
            return await _dbcontext.Users.Where(u => u.CustomerId == customerId)
                .Select(u => u.Balance)
                .FirstOrDefaultAsync(ct);
        }
        public async Task<decimal?> GetBallanceByExpertId(int expertId, CancellationToken ct)
        {
            return await _dbcontext.Users.Where(u => u.ExpertId == expertId)
               .Select(u => u.Balance)
               .FirstOrDefaultAsync(ct);
        }
        public async Task<bool> UpdateBallanceForCustomer(int customerId, decimal ballance,CancellationToken ct) 
        {
             return await _dbcontext.Users.Where(u=>u.CustomerId == customerId)
                .ExecuteUpdateAsync(set=>set.SetProperty(u=>u.Balance, ballance), ct)>0;
        }
        public async Task<bool> UpdateBallanceForExpert(int expertId, decimal ballance, CancellationToken ct)
        {
            return await _dbcontext.Users.Where(u=>u.ExpertId== expertId)
                .ExecuteUpdateAsync(set => set.SetProperty(u => u.Balance, ballance), ct) > 0;
        }
       
    }

   
}
