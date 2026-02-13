namespace Asistant.Areas.Expert.Models
{
    public class RequestViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }


        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
       
        public string? Status { get; set; }
        
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? HomeServiceName { get; set; }
        public int? HomeServiceId { get; set; }
        
        public List<string>? ImagesPath { get; set; }
      
    }
}
