namespace Asistant.Areas.Admin.Models
{
    public class PagedViewModel<T,T2>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public T2? MyProp { get; set; }
    }
}
