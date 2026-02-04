using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.DTOs
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public int UserId { get; set; }
        public int? CityId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
        public int? ImageId { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
    }
}
