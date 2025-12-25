using Asistant_Domain_Core.CommentAgg.Entity;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.Entities
{
    public class HomeService
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
        public List<Expert> Experts { get; set; }
        public List<Request> Requests { get; set; }
        public List<Suggestion> Suggestions { get; set; }
        #endregion
    }
}
