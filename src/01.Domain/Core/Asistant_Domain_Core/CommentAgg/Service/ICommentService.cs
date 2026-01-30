using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.CommentAgg.Service
{
    public interface ICommentService
    {
        Task<bool> DeleteByCustumerId(CancellationToken ct, int id);
        Task<bool> DeleteByExpertId(CancellationToken ct, int id);
    }
}
