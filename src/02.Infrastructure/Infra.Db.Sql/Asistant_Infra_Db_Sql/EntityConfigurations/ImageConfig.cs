using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.ImageAgg.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.EntityConfigurations
{
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(
                new Image
                {
 
               Id=1,
               ImagePath="\\Images\\Category\\1.jpg",
               CategoryId=1,
               ImageCategory=ImageEnum.Category,
           
                },
                new Image
                {
                    Id=2,
                    ImagePath="\\Images\\Category\\2.jpg",
                    CategoryId=2,
                    ImageCategory=ImageEnum.Category,
                },
                new Image
                {
                    Id=3,
                    ImagePath="\\Images\\Category\\3.jpg"
                    ,CategoryId=3,
                    ImageCategory= ImageEnum.Category,

                },
                new Image
                {
                    Id =4,
                    ImagePath="\\Images\\Category\\4.jpg",
                    CategoryId=4,
                    ImageCategory=ImageEnum.Category,
                },
                new Image
                {
                    Id=5,
                    ImagePath="\\Images\\Category\\5.jpg",
                    CategoryId=5,
                    ImageCategory=ImageEnum.Category,
                }
                ,new Image
                {
                    Id=6,
                    ImagePath="\\Images\\Category\\6.jpg",
                    CategoryId=6,
                    ImageCategory=ImageEnum.Category,
                },
                new Image
                {
                    Id=7
                    ,ImagePath= "\\Images\\Category\\7.jpg",
                    CategoryId=7,
                    ImageCategory= ImageEnum.Category,
                },
                new Image
                {
                    Id = 8
                    ,
                    ImagePath = "\\Images\\Category\\8.jpg",
                    CategoryId = 8,
                    ImageCategory = ImageEnum.Category,

                },
                 new Image
                 {
                     Id = 9
                    ,
                     ImagePath = "\\Images\\Category\\9.jpg",
                     CategoryId = 9,
                     ImageCategory = ImageEnum.Category,

                 },
                  new Image
                  {
                      Id = 10
                    ,
                      ImagePath = "\\Images\\Category\\10.jpg",
                      CategoryId = 10,
                      ImageCategory = ImageEnum.Category,

                  },
                   new Image
                   {
                       Id = 11 ,
                       ImagePath = "\\Images\\HomeService\\4-3-3.jpg",
                       HomeServiceId = 1,
                       ImageCategory = ImageEnum.HomeService,
                   },
                    new Image
                    {
                        Id = 12,
                        ImagePath = "\\Images\\HomeService\\download.jpg",
                        HomeServiceId = 2,
                        ImageCategory = ImageEnum.HomeService,
                    },
                     new Image
                     {
                         Id = 13,
                         ImagePath = "\\Images\\HomeService\\cleantoilet.jpg",
                         HomeServiceId = 3,
                         ImageCategory = ImageEnum.HomeService,
                     },
                     new Image
                     {
                         Id = 14,
                         ImagePath = "\\Images\\HomeService\\fixlaundry.jpg",
                         HomeServiceId = 4,
                         ImageCategory = ImageEnum.HomeService,
                     },
                      new Image
                      {
                          Id = 15,
                          ImagePath = "\\Images\\HomeService\\fixrefrig.jpg",
                          HomeServiceId = 5,
                          ImageCategory = ImageEnum.HomeService,
                      },
                      new Image
                      {
                          Id = 16,
                          ImagePath = "\\Images\\HomeService\\download (1).jpg",
                          HomeServiceId = 6,
                          ImageCategory = ImageEnum.HomeService,
                      },
                      new Image
                      {
                          Id = 17,
                          ImagePath = "\\Images\\HomeService\\download (2).jpg",
                          HomeServiceId = 7,
                          ImageCategory = ImageEnum.HomeService,
                      },
                      new Image
                      {
                          Id = 18,
                          ImagePath = "\\Images\\HomeService\\download (3).jpg",
                          HomeServiceId = 8,
                          ImageCategory = ImageEnum.HomeService,
                      },
                       new Image
                       {
                           Id = 19,
                           ImagePath = "\\Images\\HomeService\\download (4).jpg",
                           HomeServiceId = 9,
                           ImageCategory = ImageEnum.HomeService,
                       },
                        new Image
                        {
                            Id = 20,
                            ImagePath = "\\Images\\HomeService\\download (5).jpg",
                            HomeServiceId = 10,
                            ImageCategory = ImageEnum.HomeService,
                        },
                        new Image
                        {
                            Id = 21,
                            ImagePath = "\\Images\\HomeService\\download (6).jpg",
                            HomeServiceId = 11,
                            ImageCategory = ImageEnum.HomeService,
                        },
                         new Image
                         {
                             Id = 22,
                             ImagePath = "\\Images\\HomeService\\download (7).jpg",
                             HomeServiceId = 12,
                             ImageCategory = ImageEnum.HomeService,
                         },
                          new Image
                          {
                              Id = 23,
                              ImagePath = "\\Images\\HomeService\\download (8).jpg",
                              HomeServiceId = 13,
                              ImageCategory = ImageEnum.HomeService,
                          },
                            new Image
                            {
                                Id = 24,
                                ImagePath = "\\Images\\HomeService\\download (9).jpg",
                                HomeServiceId = 14,
                                ImageCategory = ImageEnum.HomeService,
                            },
                             new Image
                             {
                                 Id = 25,
                                 ImagePath = "\\Images\\HomeService\\download (10).jpg",
                                 HomeServiceId = 15,
                                 ImageCategory = ImageEnum.HomeService,
                             },
                             new Image
                             {
                                 Id = 26,
                                 ImagePath = "\\Images\\HomeService\\download (11).jpg",
                                 HomeServiceId = 16,
                                 ImageCategory = ImageEnum.HomeService,
                             },
                             new Image
                             {
                                 Id = 27,
                                 ImagePath = "\\Images\\HomeService\\download (12).jpg",
                                 HomeServiceId = 17,
                                 ImageCategory = ImageEnum.HomeService,
                             },
                              new Image
                              {
                                  Id = 28,
                                  ImagePath = "\\Images\\HomeService\\download (13).jpg",
                                  HomeServiceId = 18,
                                  ImageCategory = ImageEnum.HomeService,
                              },
                              new Image
                              {
                                  Id = 29,
                                  ImagePath = "\\Images\\HomeService\\download (14).jpg",
                                  HomeServiceId = 19,
                                  ImageCategory = ImageEnum.HomeService,
                              },
                               new Image
                               {
                                   Id = 30,
                                   ImagePath = "\\Images\\HomeService\\download (15).jpg",
                                   HomeServiceId = 20,
                                   ImageCategory = ImageEnum.HomeService,
                               },
                               new Image
                               {
                                   Id = 31,
                                   ImagePath = "\\Images\\HomeService\\download (16).jpg",
                                   HomeServiceId = 21,
                                   ImageCategory = ImageEnum.HomeService,
                               }
                );
           
        }
    }
}
