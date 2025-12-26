using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.SuggestionAgg.Enums;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.SuggestionAgg.Entity
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
        public StatusEnum Status { get; set; }
        #region Navigation Prop
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
       
        public Request Request  { get; set; }
        public int RequestId {  get; set; }
        public List<Image>? Images { get; set; }
        #endregion
    }
}
