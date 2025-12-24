using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Enums;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UtilityAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.Entity
{
    public class Request
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime? VerifyExpertDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        #region Navigation Prop
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Utility Utility { get; set; }
        public int UtilityId { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public List<Image>? Images { get; set; }
        #endregion
    }
}
