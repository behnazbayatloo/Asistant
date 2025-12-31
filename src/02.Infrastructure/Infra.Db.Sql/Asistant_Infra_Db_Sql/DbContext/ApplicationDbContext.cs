using Asistant_Domain_Core.CommentAgg.Entity;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Db_Sql.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get;set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Request> Requests { get;set; }
        public DbSet<City> Cities { get; set; }
        
    }
}