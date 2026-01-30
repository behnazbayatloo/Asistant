using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asistant.Areas.Admin.Models
{
    public class EditHomeServiceViewModel
    {
        public int Id { get; set; }

       
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        public decimal? BasePrice { get; set; }
        
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? ImageId { get; set; }
       
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}

