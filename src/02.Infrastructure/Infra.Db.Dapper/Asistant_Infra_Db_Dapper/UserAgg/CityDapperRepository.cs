using Asistant_Domain_Core.Configurations;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Dapper.UserAgg
{
    public class CityDapperRepository(SiteSettings siteSettings) : ICityDapperRepository
    {
        private readonly string connectionString = siteSettings.ConnectionStringsConfiguration.DefaultConnection;

        public async Task<List<City>> GetAllCities(CancellationToken ct)
        {
            using var cn = new SqlConnection(connectionString);
            await cn.OpenAsync(ct);
            var query = "SELECT Id,Name FROM Cities c WHERE c.IsDeleted=0";
            var cmd = new CommandDefinition(query, parameters: null, cancellationToken: ct); 
            var result = await cn.QueryAsync<City>(cmd); 
            return result.ToList();
        }

     
        
    }
}
