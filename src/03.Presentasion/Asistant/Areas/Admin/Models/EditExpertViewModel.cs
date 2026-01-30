using Asistant_FrameWork.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Admin.Models
{
    public class EditExpertViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل معتبر نیست")]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
        [RequireUppercase]
        public string? Password { get; set; }
        public int UserId { get; set; }
        public int? CityId { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
      
        public IEnumerable<SelectListItem>? Cities { get; set; }
        public List<int>? HomeServicesIds { get; set; }
        public IEnumerable<SelectListItem>? HomeServices {  get; set; }
    }
}
