using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_Domain_Core.CommentAgg.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CommentAppService(ICommentService _cmtserv,ILogger<CommentAppService> logger):ICommentAppService
    {
    }
}
