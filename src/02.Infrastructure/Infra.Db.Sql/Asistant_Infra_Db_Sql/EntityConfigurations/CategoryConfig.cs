using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Entity;
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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(500);
            builder.HasMany(c => c.HomeServices).WithOne(hs => hs.Category).HasForeignKey(hs => hs.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Image).WithOne(i => i.Category).HasForeignKey<Image>(i => i.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "نظافت منزل",
                    
                }
                , new Category
                {
                    Id = 2,
                    Name = "تعمیرات لوازم خانگی"
                },
                new Category
                {
                    Id = 3,
                    Name = "خدمات برقکاری"
                },
                new Category
                {
                    Id = 4,
                    Name = "خدمات لوله‌کشی"
                },
                new Category
                {
                    Id = 5,
                    Name = "خدمات نقاشی و دکوراسیون"
                },
                new Category
                {
                    Id = 6,
                    Name = "خدمات باغبانی"
                },
                new Category
                {
                    Id = 7,
                    Name = "خدمات کامپیوتر و شبکه"
                },
                new Category
                {
                    Id = 8,
                    Name = "خدمات خودرو"
                },
                new Category
                {
                    Id = 9,
                    Name = "خدمات آموزشی"
                },
                new Category
                {
                    Id = 10,
                    Name = "خدمات پزشکی و پرستاری"
                });
        }
    }
}
