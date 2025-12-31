using Asistant_Domain_Core.HomeServiceAgg.DTOs;

namespace Asistant.Models
{
    public class CategoryHomeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<HomeServiceHomeViewModel> HomeServices { get; set; }
    }
}
