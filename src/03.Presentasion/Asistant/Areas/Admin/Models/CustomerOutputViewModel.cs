namespace Asistant.Areas.Admin.Models
{
    public class CustomerOutputViewModel
    {
     
            public int Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string Email { get; set; }
            public string? Adress { get; set; }
            public int UserId { get; set; }
        public string? CityName { get; set; }
        public decimal? Ballance { get; set; }


    }
}
