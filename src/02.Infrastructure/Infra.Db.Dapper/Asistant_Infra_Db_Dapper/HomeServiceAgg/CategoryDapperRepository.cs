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
    public class CategoryDapperRepository(SiteSettings siteSettings) : ICategoryDapperRepository
    {
        private readonly string connectionString = siteSettings.ConnectionStringsConfiguration.DefaultConnection;

        public async Task<IEnumerable<GetCategoryDTO>> GetAllCtegories(CancellationToken ct)
        {
            using var cn = new SqlConnection(connectionString);
            await cn.OpenAsync(ct);
            var sql =
                @" SELECT c.Id, c.Name, i.ImagePath, hs.Id, hs.Name,
                  hs.CategoryId FROM Categories c 
                    LEFT JOIN HomeServices hs ON hs.CategoryId = c.Id
                    LEFT JOIN Images i ON i.CategoryId = c.Id ";
            var cmd = new CommandDefinition(sql, cancellationToken: ct);
            var categoryDict = new Dictionary<int, GetCategoryDTO>();
            var result = await cn.QueryAsync<GetCategoryDTO, HomeServiceDTO, GetCategoryDTO>
                (cmd, (category, homeService) =>
                {
                    if (!categoryDict.TryGetValue(category.Id, out var currentCategory))
                    {
                        currentCategory = category;
                        currentCategory.HomeServices = new List<HomeServiceDTO>();
                        categoryDict.Add(currentCategory.Id, currentCategory);
                    }
                    if (homeService != null)
                    {
                        currentCategory.HomeServices.Add(homeService);
                    } 
                    return currentCategory;
                },
                splitOn: "Id" );
                
            return categoryDict.Values.ToList();
        }
    }
}
