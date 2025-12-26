using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
          
            builder.HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole<int>
                {
                    Id = 3,
                    Name = "Expert",
                    NormalizedName = "EXPERT"
                }
                );
            }
    }
}
