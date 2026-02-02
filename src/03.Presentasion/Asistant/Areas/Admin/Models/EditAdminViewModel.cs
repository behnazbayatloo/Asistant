namespace Asistant.Areas.Admin.Models
{
    public class EditAdminViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public string? Email { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
    }
}
