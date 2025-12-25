using Asistant_Domain_Core.HomeServiceAgg.Entities;
using Asistant_Domain_Core.ImageAgg.Enum;
using Asistant_Domain_Core.RequestAgg.Entity;
using Asistant_Domain_Core.SuggestionAgg.Entity;
using Asistant_Domain_Core.UserAgg.Entities;

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
        public string ImagePath { get; set; }    
        public ImageEnum ImageCategory { get; set; }

        #region Navigation 
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Expert Expert { get; set; }
        public int ExpertId { get; set; }
        public Request Request {  get; set; }
        public int RequestId { get; set; }
        public Suggestion Suggestion { get; set; }
        public int SuggestionId { get;set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public HomeService HomeService{ get; set; }
        public int HomeServiceId { get; set; }

        #endregion
    }
}
