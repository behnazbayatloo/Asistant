using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(
                new City
                {
                    Id = 1,
                    Name = "تهران"
                },
                new City
                {
                    Id = 2,
                    Name = "مشهد"
                },
                new City
                {
                    Id = 3,
                    Name = "اصفهان"
                },
                new City
                {
                    Id = 4,
                    Name = "شیراز"
                },
                new City
                {
                    Id = 5
                    ,
                    Name = "تبریز"
                },
                new City
                {
                    Id = 6,
                    Name = "کرج"
                },
                new City
                {
                    Id = 7,
                    Name = "قم"
                },
                new City
                {
                    Id = 8,
                    Name = "اهواز"
                },
                new City
                {
                    Id = 9,
                    Name = "کرمانشاه"
                },
                new City
                {
                    Id = 10,
                    Name = "رشت"
                }, new City
                {
                    Id = 11,
                    Name = "یزد"
                },
                new City
                {
                    Id = 12,
                    Name = "کرمان"
                }, new City
                {
                    Id = 13,
                    Name = "ارومیه"
                },
                new City
                {
                    Id = 14,
                    Name = "زاهدان"
                },
                new City
                {
                    Id = 15,
                    Name = "ساری"
                });
        }

    }
}
