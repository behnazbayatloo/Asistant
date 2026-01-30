using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core._commonEntities
{
    public class LoginResult
    {
        public bool Result { get; set; }
        public string? Message { get; set; }
        public string? Role { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
