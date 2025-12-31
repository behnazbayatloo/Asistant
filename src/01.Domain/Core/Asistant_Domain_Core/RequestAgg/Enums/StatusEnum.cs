using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.RequestAgg.Enums
{
    public enum StatusEnum
    {
        PendingExpertApproval= 1,
        PendingSuggestionApproval=2,
        AwaitingExpertArrivalOnSite=3,
        InProgress=4,
        Completed=5,
        RejectedByAdmin = 6
    }
}
