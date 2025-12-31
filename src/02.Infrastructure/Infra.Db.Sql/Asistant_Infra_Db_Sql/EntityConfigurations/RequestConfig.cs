using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.RequestAgg.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class RequestConfig : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasMany(r => r.Images).
                WithOne(i => i.Request)
                .HasForeignKey(i => i.RequestId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r=>r.HomeService)
                .WithMany(hs=>hs.Requests)
                .HasForeignKey(r=>r.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(r => r.Suggestions)
                .WithOne(s => s.Request)
                .HasForeignKey(r => r.RequestId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(ap => !ap.IsDeleted);
        }
    }
}
