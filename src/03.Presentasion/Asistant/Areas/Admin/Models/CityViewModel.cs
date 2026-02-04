using System.ComponentModel.DataAnnotations;

namespace Asistant.Areas.Admin.Models
{
    public class CityViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage ="نام شهر الزامیست")]
        public string Name { get; set; }
    }
}
