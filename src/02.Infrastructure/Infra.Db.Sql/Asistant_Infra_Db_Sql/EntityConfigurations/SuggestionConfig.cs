using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class SuggestionConfig : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.HasIndex(s => new { s.RequestId, s.ExpertId })
                .IsUnique();
            builder.HasMany(s => s.Images).
                WithOne(i => i.Suggestion).
                HasForeignKey(i => i.SuggestionId).
                OnDelete(DeleteBehavior.NoAction);
            builder.HasQueryFilter(ap => !ap.IsDeleted);
        }
    }
}
