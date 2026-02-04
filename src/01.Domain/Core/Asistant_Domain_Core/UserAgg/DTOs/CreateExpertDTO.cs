using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.DTOs
{
    public class CreateExpertDTO
    {
        
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }    
        public int UserId { get; set; }
        public List<int>? HomeServicesId { get; set; }
        public int? CityId { get; set; }
        public List<HomeService>? HomeServices { get; set; }
        public int? ImageId { get; set; }
        public decimal? Balance { get; set; }
    }
}
