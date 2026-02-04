using Asistant_FrameWork.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Admin.Models
{
    public class CreateCustomerViewModel
    {
        public int? Id { get; set; }
        
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
        [RequireUppercase]
        [Required(ErrorMessage = "وارد کردن رمزعبور الزامیست")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
     
        [Required(ErrorMessage = "وارد کردن ایمیل الزامیست")]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
      public decimal? Ballance { get; set; }
        public IFormFile? Image { get; set; }
        public int? CityId { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
    }
}
