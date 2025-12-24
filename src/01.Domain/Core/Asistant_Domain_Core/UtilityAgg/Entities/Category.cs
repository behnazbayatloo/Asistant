using Asistant_Domain_Core.ImageAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UtilityAgg.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Navigation Prop
        public List<Utility> Utilities { get; set; }
        public Image Image { get; set; }
        public int ImageId { get; set; }
        #endregion
    }
}
