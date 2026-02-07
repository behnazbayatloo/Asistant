using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.CommentAgg.DTOs
{
    public class InputCommentDTO
    {
        public string? Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
      
        public int CustomerId { get; set; }
      
        public int ExpertId { get; set; }

        public int RequestId { get; set; }
    
        public int HomeServiceId { get; set; }
    }
}
