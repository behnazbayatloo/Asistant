namespace Asistant.Areas.Customer.Models
{
    public class SuggestionViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
        public decimal? Price { get; set; }

        public string? Status { get; set; }

        public int? ExpertId { get; set; }
        public string? ExpertName { get; set; }

        public int? RequestId { get; set; }
        public List<int>? ImagesId { get; set; }
        public List<string>? ImagesPath { get; set; }
    }
}
