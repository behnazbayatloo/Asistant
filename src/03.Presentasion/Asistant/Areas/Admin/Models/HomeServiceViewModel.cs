using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Admin.Models
{
    public class HomeServiceViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "وارد کردن نام الزامیست")]
        public string Name { get; set; }
        [Required(ErrorMessage = "وارد کردن توضیحات الزامیست")]
        public string Description { get; set; }
        [Required(ErrorMessage = "وارد کردن قیمت پایه الزامیست")]
        public decimal BasePrice { get; set; }
        [Required(ErrorMessage = "انتخاب دسته بندی الزامیست")]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? ImageId { get; set; }
        [Required(ErrorMessage = "انتخاب عکس الزامیست")]
        public IFormFile Image { get; set; }
        public string? ImagePath { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
