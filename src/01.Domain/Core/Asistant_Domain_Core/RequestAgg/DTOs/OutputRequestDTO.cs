using Asistant_Domain_Core.CommentAgg.DTOs;
using Asistant_Domain_Core.CommentAgg.Entity;
using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.DTOs
{
    public class OutputRequestDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }


        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AppointmentReadyDate { get; set; }
        public string Status { get; set; }
        public DateTime? VerifyExpertDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        

  
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string HomeServiceName { get; set; }
        public int HomeServiceId { get; set; }
        public List<int>? SuggestionsId { get; set; }
        public List<int>? ImagesId { get; set; }
        public List<string>? ImagesPath { get; set; }
        public CommentDTO? Comment { get; set; }
        public int? CommentId { get; set; }
        public int SuggesstionCount { get; set; }
        public decimal? BasePriceHomeService { get; set; }
      
    }
}
