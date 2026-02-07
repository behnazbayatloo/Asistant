namespace Asistant.Areas.Customer.Models
{
    public class CreateCommentViewModel
    {
        public InputCommentViewModel Comment { get; set; }
        public SuggestionViewModel? Suggestion { get; set; }
        public RequestViewModel? Request { get; set; }
    }
}
