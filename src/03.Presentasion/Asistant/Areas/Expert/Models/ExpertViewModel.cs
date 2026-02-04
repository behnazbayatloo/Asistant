using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asistant.Areas.Expert.Models
{
    public class ExpertViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public int UserId { get; set; }
        public string? CityName { get; set; }
        public List<string>? HomeServiceNames { get; set; }
        public string? ImagePath { get; set; }
        public decimal? Ballance { get; set; }
    }
}
