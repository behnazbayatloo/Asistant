using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.UserAgg.DTOs
{
    public class RegisterDTO
    {
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
    }
}
