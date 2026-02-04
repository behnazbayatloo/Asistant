namespace Asistant.Areas.Customer.Models
{
    public class HomeServiceViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
    }
}
