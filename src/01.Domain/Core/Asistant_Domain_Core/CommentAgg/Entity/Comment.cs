using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UtilityAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.CommentAgg.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        #region Navigation Prop
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Expert Expert { get; set; }
        public int ExpertId { get; set; }
        public Utility Utility { get; set; }
        public int UtilityId { get; set; }
        #endregion
    }
}
