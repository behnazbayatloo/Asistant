using Asistant_Domain_Core.Configurations;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.UserAgg.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Dapper.HomeServiceAgg
{
    public class HomeServiceDapperRepository(SiteSettings siteSettings): IHomeServiceDapperRepository
    {
        private readonly string connectionString = siteSettings.ConnectionStringsConfiguration.DefaultConnection;

        public async Task<IEnumerable<GetHomeServiceDTO>> GetAllHomeServices(CancellationToken ct)
        {
            using var cn = new SqlConnection(connectionString);
            var query = @"SELECT hs.Id,hs.Name,hs.Description,hs.BasePrice,hs.CategoryId,hs.ImageId,
                  i.ImagePath,c.Name AS CategoryName
                  FROM HomeServices hs
                    LEFT JOIN Categories c ON c.Id = hs.CategoryId
                    LEFT JOIN Images i ON i.HomeServiceId = hs.Id";
            var cmd = new CommandDefinition(query, parameters: null, cancellationToken: ct);
            var result = cn.Query<GetHomeServiceDTO>(cmd);
            return result.ToList();
        }
    }
}
