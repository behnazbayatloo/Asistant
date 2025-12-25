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
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            PasswordHasher<IdentityUser<int>> passwordHasher = new PasswordHasher<IdentityUser<int>>();
            var user = new AppUser
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "09351650512",
                NormalizedUserName = "09351650512",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                SecurityStamp = new string(Guid.NewGuid().ToString()),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "A12345");
            var customer1 = new AppUser
            {
                Id = 2,
                FirstName = "بهناز",
                LastName = "بیاتلو",
                UserName = "behnaz",
                NormalizedUserName = "BEHNAZ",
                Email = "behnaz@gmail.com",
                NormalizedEmail = "BEHNAZ@GMAIL.COM",
                ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                SecurityStamp = new string(Guid.NewGuid().ToString()),
                Balance= 6000000,
                CustomerId=1
                
            };
            customer1.PasswordHash = passwordHasher.HashPassword(customer1, "A12345");
            var customer2 = new AppUser
            {
                Id = 3,
                FirstName = "حسن",
                LastName = "اسدی",
                UserName = "hasan",
                NormalizedUserName = "HASAN",
                Email = "hasan@gmail.com",
                NormalizedEmail = "HASAN@GMAIL.COM",
                ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                SecurityStamp = new string(Guid.NewGuid().ToString()),
                Balance=4000000,
                CustomerId = 2
              
            };
            customer2.PasswordHash = passwordHasher.HashPassword(customer2, "A12345");
            
            var expert1 =
   new AppUser
   {
       Id = 4,
       FirstName = "محمد",
       LastName = "اکبری",
       UserName = "mohammad",
       NormalizedUserName = "MOHAMMAD",
       Email = "mohammad@gmail.com",
       NormalizedEmail = "MOHAMMAD@GMAIL.COM",
       ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
       SecurityStamp = new string(Guid.NewGuid().ToString()),
       ExpertId=1
      

   };
            expert1.PasswordHash = passwordHasher.HashPassword(expert1, "A12345");
            var expert2 =
               new AppUser
               {
                   Id = 5,
                   FirstName = "مجید",
                   LastName = "بیگی",
                   UserName = "majid",
                   NormalizedUserName = "MAJID",
                   Email = "majid@gmail.com",
                   NormalizedEmail = "MAJID@GMAIL.COM",
                   ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                   SecurityStamp = new string(Guid.NewGuid().ToString()),
                   ExpertId=2

               };
            expert2.PasswordHash = passwordHasher.HashPassword(expert2, "A12345");
            var expert3 =
               new AppUser
               {
                   Id = 6,
                   FirstName = "میثم",
                   LastName = "محسنی",
                   UserName = "meysam",
                   NormalizedUserName = "MEYSAM",
                   Email = "meysam@gmail.com",
                   NormalizedEmail = "MEYSAM@GMAIL.COM",
                   ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                   SecurityStamp = new string(Guid.NewGuid().ToString()),
                  ExpertId = 3

               };
            expert3.PasswordHash = passwordHasher.HashPassword(expert3, "A12345");
            var expert4 =
               new AppUser
               {
                   Id = 7,
                   FirstName = "سامان",
                   LastName = "جلیلی",
                   UserName = "saman",
                   NormalizedUserName = "SAMAN",
                   Email = "saman@gmail.com",
                   NormalizedEmail = "SAMAN@GMAIL.COM",
                   ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                   SecurityStamp = new string(Guid.NewGuid().ToString()),
                   ExpertId=4

               };
            expert4.PasswordHash = passwordHasher.HashPassword(expert4, "A12345");
            var expert5 =
               new AppUser
               {
                   Id = 8,
                   FirstName = "سارا",
                   LastName = "دلشاد",
                   UserName = "sara",
                   NormalizedUserName = "SARA",
                   Email = "sara@gmail.com",
                   NormalizedEmail = "SARA@GMAIL.COM",
                   ConcurrencyStamp = new string(Guid.NewGuid().ToString()),
                   SecurityStamp = new string(Guid.NewGuid().ToString()),
                   ExpertId=5

               };
            expert5.PasswordHash = passwordHasher.HashPassword(expert5, "A12345");
            


            builder.HasData(user ,customer1,customer2,expert1,expert2,expert3,expert4,expert5);
            builder.ToTable(name: "AspNetUsers", schema: "user");
            builder.Property(au => au.FirstName).HasMaxLength(400);
            builder.Property(au => au.LastName).HasMaxLength(400);
            builder.Property(au => au.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
