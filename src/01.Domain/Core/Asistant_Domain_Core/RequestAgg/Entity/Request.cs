using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.RequestAgg.Enums;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asistant_Domain_Core.CommentAgg.Entity;

namespace Asistant_Domain_Core.RequestAgg.Entity
{
    public class Request
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AppointmentReadyDate { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime? VerifyExpertDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigation Prop
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public HomeService HomeService { get; set; }
        public int HomeServiceId { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public List<Image>? Images { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
        #endregion
    }
}
