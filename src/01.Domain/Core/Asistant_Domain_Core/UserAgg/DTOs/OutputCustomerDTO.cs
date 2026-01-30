using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.DTOs
{
    public class OutputCustomerDTO
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
