using Asistant_Domain_Core.CommentAgg.Entity;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UtilityAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Entities
{
    public class Expert:AppUser
    {
        #region Navigation Prop
        public City City { get; set; }
        public int CityId { get; set; }
        public Image? Image { get; set; }
        public int? ImageId { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public List<Utility>? Utilities { get; set; }
        public List<Comment> Comments { get; set; }
        #endregion
    }
}
