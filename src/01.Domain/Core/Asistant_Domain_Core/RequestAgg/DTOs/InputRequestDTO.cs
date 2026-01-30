using Asistant_Domain_Core.CommentAgg.Entity;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.DTOs
{
    public class InputRequestDTO
    {
       
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AppointmentReadyDate { get; set; }
      
   
        public int CustomerId { get; set; }
     
        public int HomeServiceId { get; set; }
       
        public List<IFormFile>? Images { get; set; }
        public List<string>? ImagesPath {  get; set; }
        
    }
}
