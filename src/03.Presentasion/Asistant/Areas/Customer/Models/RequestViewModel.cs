using Asistant_Domain_Core.CommentAgg.DTOs;

namespace Asistant.Areas.Customer.Models
{
    public class RequestViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }


        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
        public string? AppointmentReadyDate { get; set; }
        public string? Status { get; set; }
        public string? VerifyExpertDate { get; set; }
        public string? CompletedDate { get; set; }



        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? HomeServiceName { get; set; }
        public int? HomeServiceId { get; set; }
        public List<int>? SuggestionsId { get; set; }
        public List<int>? ImagesId { get; set; }
        public List<string>? ImagesPath { get; set; }
        public CommentViewModel? Comment { get; set; }
        public int? CommentId { get; set; }
        public int? SuggesstionCount { get; set; }
    }
}
