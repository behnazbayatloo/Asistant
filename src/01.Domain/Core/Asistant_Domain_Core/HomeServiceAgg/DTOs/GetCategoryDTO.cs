using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.HomeServiceAgg.DTOs
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<HomeServiceDTO> HomeServices { get; set; }
    }
}
