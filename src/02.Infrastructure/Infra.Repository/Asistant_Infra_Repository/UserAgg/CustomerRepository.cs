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
    public class CustomerRepository(ApplicationDbContext _dbcontext):ICustomerRepository
    {
        public async Task<PagedResult<OutputCustomerDTO>> GetAllCustumers(int pageNumber, int pageSize,CancellationToken ct)
        {
           
            var query =  _dbcontext.Customers
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c=> new OutputCustomerDTO
          {
              Id=c.Id,
              Adress=c.Address,
              UserId=c.UserId,
              Email=c.User.Email,
              FirstName=c.User.FirstName,
              LastName=c.User.LastName,
              CityName=c.City.Name
          });
            
            return await query.ToPaginatedResult<OutputCustomerDTO>(pageNumber,pageSize,ct);
        }
        public async Task<bool> DeleteCustomer(CancellationToken ct, int id)
        {
            return await _dbcontext.Customers.Where(c => c.Id == id)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsDeleted, true), ct)>0;
        }
        public async Task<OutputCustomerDTO?> GetCustomerById(CancellationToken ct, int id)
        {
            return await _dbcontext.Customers.AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new OutputCustomerDTO {
                    Id=c.Id,
                    Adress=c.Address,
                    CityId=c.CityId,
                    Email=c.User.Email,
                    FirstName=c.User.FirstName,
                    LastName=c.User.LastName,
                    UserId=c.UserId,
                    CityName=c.City.Name,
                    ImagePath=c.Image.ImagePath
                
                }).FirstOrDefaultAsync(ct);
        }
        public async Task<bool> UpdateCustomer(CancellationToken ct, UpdateCustomerDTO updatedCustomer)
        {
           return await _dbcontext.Customers.Where(c => c.Id == updatedCustomer.Id)
                .ExecuteUpdateAsync(c =>
                c
                .SetProperty(c=>c.CityId,c=> updatedCustomer.CityId ?? c.CityId)
                .SetProperty(c=>c.Address,c=> updatedCustomer.Adress ?? c.Address)
                .SetProperty(c=>c.ImageId,c=> updatedCustomer.ImageId ?? c.ImageId)
                ,ct
                ) > 0;



        }
        public async Task<int> CreateCustomer(int userId,CancellationToken ct)
        {
            var customer = new Customer
            {
                UserId = userId,
            };
                  await _dbcontext.Customers.AddAsync(customer,ct);
           await _dbcontext.SaveChangesAsync(ct);
            return customer.Id;
        }
        public async Task<int> CreateCustomerByAdmin(CreateCustomerDTO customerDTO,CancellationToken ct)
        {
            var customer = new Customer
            {
                Address = customerDTO.Address,
                CityId= customerDTO.CityId,
                UserId = customerDTO.UserId
            };

            await _dbcontext.Customers.AddAsync(customer,ct);
            await _dbcontext.SaveChangesAsync(ct);
            return customer.Id;
        }
        public async Task<bool> UpdateImageId (int id,int imageId,CancellationToken ct)
        {
            return await _dbcontext.Customers.Where(c=>c.Id==id)
                .ExecuteUpdateAsync(setter=>setter.SetProperty(c=>c.ImageId,imageId),ct)>0;
        }

        public async Task<OutputCustomerDTO?> GetCustomerByUserId(int userId,CancellationToken ct)
        {
            return await _dbcontext.Customers.Where(c => c.UserId == userId)
                .Select(c => new OutputCustomerDTO
                {
                    Id = c.Id,
                    Adress = c.Address,
                    CityId = c.CityId,
                    CityName = c.City.Name,
                    ImagePath = c.Image.ImagePath,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    Email = c.User.Email
                }).FirstOrDefaultAsync(ct);
        }
    }
}
