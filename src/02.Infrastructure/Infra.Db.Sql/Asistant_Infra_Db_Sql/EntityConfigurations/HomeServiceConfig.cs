using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class HomeServiceConfig : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.HasOne(hs=>hs.Image).WithOne(i=>i.HomeService).HasForeignKey<Image>(i=>i.HomeServiceId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(hs=>hs.Comments).WithOne(c=>c.HomeService).HasForeignKey(c=>c.HomeServiceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
                new HomeService
                {
                    Id = 1,
                    Name = "نظافت آشپزخانه",
                    Description = "تمیزکاری کامل کابینت‌ها، کف و دیوارها", 
                    BasePrice = 300000,
                    CategoryId = 1 ,
                    ImageId= 11 ,
                },
                new HomeService
                { 
                    Id = 2,
                    Name = "نظافت اتاق‌ها",
                    Description = "گردگیری و جاروکشی اتاق‌ها",
                    BasePrice = 200000, 
                    CategoryId = 1,
                ImageId=12
                },
                new HomeService 
                {
                    Id = 3,
                    Name = "نظافت سرویس بهداشتی",
                    Description = "شستشو و ضدعفونی سرویس‌ها",
                    BasePrice = 250000, 
                    CategoryId = 1 ,
                ImageId=13
                },
                new HomeService
                { 
                    Id = 4,
                    Name = "تعمیر ماشین لباسشویی",
                    Description = "عیب‌یابی و تعمیر انواع ماشین لباسشویی",
                    BasePrice = 500000, 
                    CategoryId = 2, 
                ImageId=14
                },
                new HomeService
                { 
                    Id = 5,
                    Name = "تعمیر یخچال",
                    Description = "سرویس و تعمیر یخچال و فریزر",
                    BasePrice = 600000, 
                    CategoryId = 2 ,
                    ImageId = 15
                },
                new HomeService 
                { 
                    Id = 6,
                    Name = "نصب کلید و پریز", 
                    Description = "نصب و تعویض کلید و پریز برق", 
                    BasePrice = 150000, 
                    CategoryId = 3,
                    ImageId = 16
                }, 
                new HomeService
                { 
                    Id = 7,
                    Name = "سیم‌کشی ساختمان", 
                    Description = "اجرای سیم‌کشی برق داخلی", 
                    BasePrice = 800000, 
                    CategoryId = 3,
                    ImageId = 17
                },
                new HomeService
                {
                    Id = 8, 
                    Name = "رفع گرفتگی لوله",
                    Description = "باز کردن لوله‌های فاضلاب و آب",
                    BasePrice = 250000,
                    CategoryId = 4 ,
                    ImageId = 18
                },
                new HomeService 
                { 
                    Id = 9,
                    Name = "نصب شیرآلات",
                    Description = "نصب و تعویض شیرآلات آشپزخانه و حمام",
                    BasePrice = 200000, 
                    CategoryId = 4,
                    ImageId = 19
                }, 
                new HomeService
                { 
                    Id = 10,
                    Name = "نقاشی دیوار",
                    Description = "رنگ‌آمیزی دیوارهای داخلی", 
                    BasePrice = 400000, 
                    CategoryId = 5,
                    ImageId = 20
                },
                new HomeService
                { 
                    Id = 11, 
                    Name = "کاغذ دیواری",
                    Description = "نصب انواع کاغذ دیواری",
                    BasePrice = 700000, 
                    CategoryId = 5,
                    ImageId = 21
                },
                new HomeService 
                { 
                    Id = 12
                    , Name = "هرس درختان",
                    Description = "هرس و مرتب‌سازی درختان باغ و حیاط",
                    BasePrice = 350000,
                    CategoryId = 6 ,
                    ImageId = 22
                },
                new HomeService
                { 
                    Id = 13,
                    Name = "کاشت گل و گیاه", 
                    Description = "کاشت و نگهداری گل‌ها و گیاهان", 
                    BasePrice = 300000,
                    CategoryId = 6,
                    ImageId = 23
                },
                new HomeService 
                { 
                    Id = 14,
                    Name = "نصب ویندوز",
                    Description = "نصب و راه‌اندازی سیستم عامل ویندوز",
                    BasePrice = 200000, 
                    CategoryId = 7,
                    ImageId = 24
                }, 
                new HomeService { 
                    Id = 15,
                    Name = "راه‌اندازی شبکه خانگی",
                    Description = "نصب مودم و تنظیم شبکه داخلی",
                    BasePrice = 400000,
                    CategoryId = 7,
                    ImageId = 25
                },
                new HomeService 
                {
                    Id = 16, 
                    Name = "تعویض روغن", 
                    Description = "تعویض روغن موتور خودرو",
                    BasePrice = 150000, 
                    CategoryId = 8,
                    ImageId = 26
                },
                new HomeService { 
                    Id = 17,
                    Name = "باتری‌سازی", 
                    Description = "نصب و تعمیر باتری خودرو",
                    BasePrice = 250000,
                    CategoryId = 8 ,
                    ImageId = 27
                },
                new HomeService
                { 
                    Id = 18,
                    Name = "آموزش زبان انگلیسی", 
                    Description = "کلاس خصوصی زبان انگلیسی",
                    BasePrice = 500000, 
                    CategoryId = 9,
                    ImageId = 28
                },
                new HomeService {
                    Id = 19,
                    Name = "آموزش ریاضی", 
                    Description = "کلاس تقویتی ریاضی", 
                    BasePrice = 400000,
                    CategoryId = 9,
                    ImageId = 29
                },
                new HomeService 
                { 
                    Id = 20, 
                    Name = "پرستاری در منزل", 
                    Description = "مراقبت از بیمار در منزل", 
                    BasePrice = 700000, 
                    CategoryId = 10,
                    ImageId = 30
                }, 
                new HomeService
                { 
                    Id = 21, 
                    Name = "ویزیت پزشک عمومی", 
                    Description = "ویزیت پزشک در منزل", 
                    BasePrice = 800000,
                    CategoryId = 10,
                    ImageId = 31
                }
                );
        }
    }
}
