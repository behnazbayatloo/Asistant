using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.ImageAgg.DTOs
{
    public class ProfileImageDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int? ExpertId { get; set; }
        public int? CustomerId { get; set; }
    }
}
