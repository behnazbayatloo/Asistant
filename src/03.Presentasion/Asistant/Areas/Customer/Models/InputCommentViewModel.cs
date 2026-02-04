using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Customer.Models
{
    public class InputCommentViewModel
    {
        public string? Title { get; set; }
        [Required(ErrorMessage ="وارد کردن متن کامنت الزامیست")]
        public string Description { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CustomerId { get; set; }

        public int ExpertId { get; set; }

        public int RequestId { get; set; }

        public int HomeServiceId { get; set; }
    }
}
