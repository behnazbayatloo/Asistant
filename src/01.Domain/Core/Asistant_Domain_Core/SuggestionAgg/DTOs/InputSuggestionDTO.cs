using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.SuggestionAgg.DTOs
{
    public class InputSuggestionDTO
    {
       
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
       
        public int ExpertId { get; set; }
      
        public int RequestId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<string>? ImagesPath { get; set; }

    }
}
