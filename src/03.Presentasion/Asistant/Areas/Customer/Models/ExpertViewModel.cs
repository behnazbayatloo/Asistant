namespace Asistant.Areas.Customer.Models
{
    public class ExpertViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        
        public string? CityName { get; set; }
        public List<string>? HomeServices { get; set; }
        public string? ImagePath { get; set; }
        public int? SelectedHomeService { get; set; }

    }
}
