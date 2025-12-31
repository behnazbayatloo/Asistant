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
    }
}
