
using Asistant_Domain_Core.ImageAgg.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal? Balance { get; set; }

        #region Navigation Prop
        public Customer? Customer { get; set; }
        public int? CustomerId { get; set; }
        public Expert? Expert { get; set; }
        public int? ExpertId { get; set; }
        #endregion


        #region Audit 
        public DateTime CreatedAt { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UpdatedUserId { get; set; }
        #endregion
    }
}
