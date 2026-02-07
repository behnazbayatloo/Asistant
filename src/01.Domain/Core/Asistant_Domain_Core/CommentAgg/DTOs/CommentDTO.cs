using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.CommentAgg.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string ExpertName { get; set; }
        public int ExpertId { get; set; }
        public string? RequestDescription { get; set; }
        public int RequestId { get; set; }
        public string HomeServiceName { get; set; }
        public int HomeServiceId { get; set; }
    }
}
