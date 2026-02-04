using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.UserAgg.Entities;

namespace Asistant.Areas.Customer.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }
      
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string ExpertName { get; set; }
        public int ExpertId { get; set; }
        
        public int RequestId { get; set; }
        public string HomeServiceName { get; set; }
        public int HomeServiceId { get; set; }

    }
}
