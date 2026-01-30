namespace Asistant.Areas.Admin.Models
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
       
        public string? Name { get; set; }
       
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
    }
}
