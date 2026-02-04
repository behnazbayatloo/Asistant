using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Customer.Models
{
    public class CreateRequestViewModel
    {
        public string? Title { get; set; }

        [Required(ErrorMessage = "نوشتن توضیحات الزامیست")]
        public string Description { get; set; }

      
        public DateTime AppointmentReadyDate { get; set; }

        [Required(ErrorMessage = "وارد کردن تاریخ دریافت خدمت الزامیست")]

        public string PersianDate { get; set; }
        public int? CustomerId { get; set; }

        public int HomeServiceId { get; set; }

        public List<IFormFile>? Images { get; set; }
        public HomeServiceViewModel? HomeService { get; set; }
    }
}
