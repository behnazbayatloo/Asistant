using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.ImageAgg.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int? HomeServiceId { get; set; }
        public int? CategoryId { get; set; }
    }
}
