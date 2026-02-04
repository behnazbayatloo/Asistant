namespace Asistant.Areas.Admin.Models
{
    public class ExpertOutputViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public int UserId { get; set; }
        public string? CityName { get; set; }
        public List<int>? HomeServiceIds { get; set; }
        public decimal? Ballance { get; set; }
    }
}
