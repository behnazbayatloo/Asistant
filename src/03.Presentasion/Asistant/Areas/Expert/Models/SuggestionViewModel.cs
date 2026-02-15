using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Expert.Models
{
    public class SuggestionViewModel
    {
        public string? Title { get; set; }

        [Required(ErrorMessage = "نوشتن توضیحات الزامیست")]
        public string Description { get; set; }

        [Required(ErrorMessage = "لطفا قیمت را وارد کنید")]
        public decimal Price { get; set; }
        public decimal? BasePrice { get; set; }
        public int ExpertId { get; set; }
        public int HomeServiceId { get; set; }
        public int RequestId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<string>? ImagesPath { get; set; }

        public string? RequestTitle { get; set; }
        public string? CustomerName { get; set; }
        public string? RequestCreatedAt { get; set; }

    }
}
