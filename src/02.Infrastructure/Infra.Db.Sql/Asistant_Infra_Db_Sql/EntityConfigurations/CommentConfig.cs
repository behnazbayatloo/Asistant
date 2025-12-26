using Asistant_Domain_Core.CommentAgg.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Description).HasMaxLength(4000).IsRequired(true);
            builder.HasOne(c=>c.Request).WithOne(r=>r.Comment).HasForeignKey<Comment>(c=>c.RequestId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.HomeService).WithMany(hs => hs.Comments).HasForeignKey(c => c.HomeServiceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
