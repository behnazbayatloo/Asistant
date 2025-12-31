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
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            var customer1= new Customer
            {
                Id=1,
                Address = "تهران خیابان ایت الله کاشانی کوچه بهنام پلاک 4 واحد1",
                CityId = 1,
                UserId=2
            };
            var customer2 = new Customer
            {
                Id = 2,
                Address = "شیراز بلوار سعدی کوچه پرستو پلاک 12 واحد 2 ",
                CityId = 4,
                UserId=3
            };
            builder.HasData(customer1, customer2);


            
            builder.Property(c => c.Address)
                .HasMaxLength(4000);

            builder.HasMany(c=>c.Comments)
                .WithOne(cm=>cm.Customer)
                .HasForeignKey(cm=>cm.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.City)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c=>c.Requests)
                .WithOne(r=>r.Customer)
                .HasForeignKey(r=>r.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c=>c.Image)
                .WithOne(i=>i.Customer)
                .HasForeignKey<Image>(i=>i.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.User)
                .WithOne(au => au.Customer)
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(ap => !ap.IsDeleted);
        }
    }
}
