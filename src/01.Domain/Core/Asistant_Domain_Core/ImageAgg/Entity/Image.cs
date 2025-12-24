using Asistant_Domain_Core.ImageAgg.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.ImageAgg.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }    
        public ImageEnum ImageCategory { get; set; }
    }
}
