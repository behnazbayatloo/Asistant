using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
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
    public class ExpertConfig : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            var expert1 = new Expert
            {
                Id=1,
                CityId = 1,
            };
            var expert2 = new Expert
            {
                Id = 2,
                CityId = 4,
            };
            var expert3 = new Expert
            {
                Id = 3,
                CityId = 1,
            };
            var expert4 = new Expert
            {
                Id = 4,
                CityId = 4,
            };
            var expert5 = new Expert
            {
                Id = 5,
                CityId = 1,
            };
            builder.HasData(expert1,expert2, expert3, expert4,expert5);


            builder.HasOne(e => e.City).WithMany(c => c.Experts).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.HomeServices).WithMany(hs => hs.Experts)
                     .UsingEntity<Dictionary<string, object>>
                     ("ExpertHomeService",
                     j => j.HasOne<HomeService>().WithMany().HasForeignKey("HomeServiceId").OnDelete(DeleteBehavior.NoAction),
                     j => j.HasOne<Expert>().WithMany().HasForeignKey("ExpertId").OnDelete(DeleteBehavior.NoAction),
                     j =>
                     {
                         j.HasKey("ExpertId", "HomeServiceId"); j.ToTable("ExpertHomeServices");
                     });
            builder.HasOne(e => e.Image).WithOne(i => i.Expert).HasForeignKey<Image>(i => i.ExpertId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.Suggestions).WithOne(s => s.Expert).HasForeignKey(s => s.ExpertId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.Comments).WithOne(c => c.Expert).HasForeignKey(c => c.ExpertId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
