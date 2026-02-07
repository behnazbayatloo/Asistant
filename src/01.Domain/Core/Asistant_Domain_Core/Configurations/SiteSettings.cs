using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.Configurations
{
    public class SiteSettings
    {
          public ConnectionStringsConfiguration ConnectionStringsConfiguration { get; set; }
        public SerilogConfiguration SerilogConfiguration { get; set; }
        public LoggingConfiguration LoggingConfiguration { get; set; }
        public HostSettings HostSettings { get; set; }

    }
}
