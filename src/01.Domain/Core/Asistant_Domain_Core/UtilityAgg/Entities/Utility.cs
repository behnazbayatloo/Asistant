using Asistant_Domain_Core.CommentAgg.Entity;
using Asistant_Domain_Core.ImageAgg.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UtilityAgg.Entities
{
    public class Utility
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }

        #region Navigation Prop
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Image Image { get; set; }
        public int ImageId { get; set; }
        public List<Comment> Comments { get; set; }
        #endregion
    }
}
