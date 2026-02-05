using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.SuggestionAgg.DTOs
{
    public class ApproveSuggestionDTO
    {
        public int RequestId { get; set; }
        public int ExpertId { get; set; }
        public int CustomerId { get; set; }
        public int SuggestionId { get; set; }
        public decimal Price { get; set; }
    }
}
