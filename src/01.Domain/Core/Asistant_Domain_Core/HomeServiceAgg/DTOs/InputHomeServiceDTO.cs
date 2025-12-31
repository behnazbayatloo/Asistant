using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.DTOs
{
    public class InputHomeServiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int CategoryId { get; set; }
        public int ImageId { get; set; }
    }
}
