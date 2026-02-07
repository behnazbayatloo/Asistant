using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Customer.Models
{
    public class InputCommentViewModel
    {
        public string? Title { get; set; }
        [Required(ErrorMessage ="وارد کردن متن کامنت الزامیست")]
        public string Description { get; set; }
        public int Rate { get; set; }
       

    }
}
