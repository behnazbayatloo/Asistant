using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        #region Navigation Prop
        public List<Customer> Customers { get; set; }
        public List<Expert> Experts { get; set; }
        #endregion
    }
}
