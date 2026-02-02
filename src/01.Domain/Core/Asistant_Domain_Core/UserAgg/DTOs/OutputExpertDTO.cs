using Asistant_Domain_Core.HomeServiceAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.DTOs
{
    public class OutputExpertDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }    
        public int UserId { get; set; }
        public List<int>? HomeServicesId { get; set; }
        public string? ImagePath { get; set; }
        public decimal? Balance { get; set; }
    }
}
