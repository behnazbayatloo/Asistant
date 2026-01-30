using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="وارد کردن نام الزامیست")]
        public string Name { get; set; }
        [Required(ErrorMessage ="یک عکس انتخاب کنید")]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
    }
}
