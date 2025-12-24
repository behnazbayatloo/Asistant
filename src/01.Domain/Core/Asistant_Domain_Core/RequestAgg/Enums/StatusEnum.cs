using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.Enums
{
    public enum StatusEnum
    {
        
        PendingExpertApproval=1,
        AwaitingExpertArrivalOnSite=2,
        InProgress=3,
        Completed=4,
        RejectedByAdmin = 5
    }
}
