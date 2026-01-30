using Asistant_Domain_Core.CommentAgg.Data;
using Asistant_Domain_Core.CommentAgg.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class CommentService(ICommentRepository _cmtrepo,ILogger<CommentService> logger):ICommentService
    {
        public async Task<bool> DeleteByCustumerId(CancellationToken ct, int id)
            => await _cmtrepo.DeleteByCustumerId(ct, id);
        
        public async Task<bool> DeleteByExpertId(CancellationToken ct, int id)
            => await _cmtrepo.DeleteByExpertId(ct,id);
       

    }
}
