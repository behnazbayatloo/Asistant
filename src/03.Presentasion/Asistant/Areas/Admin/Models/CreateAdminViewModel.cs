using Asistant_FrameWork.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Admin.Models
{
    public class CreateAdminViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]

        [Required(ErrorMessage = "وارد کردن ایمیل الزامیست")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
        [RequireUppercase]
        [Required(ErrorMessage = "وارد کردن رمزعبور الزامیست")]
        public string Password { get; set; }
        public decimal? Balance { get; set; }
    }
}
