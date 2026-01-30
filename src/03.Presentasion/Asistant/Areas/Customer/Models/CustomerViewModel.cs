namespace Asistant.Areas.Customer.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Adress { get; set; }
        public int UserId { get; set; }
        public string? CityName { get; set; }
        public int? CityId { get; set; }
        public string? ImagePath { get; set; }
    }
}
