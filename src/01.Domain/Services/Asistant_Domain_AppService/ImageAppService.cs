using Asistant_Domain_Core.ImageAgg.AppService;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Infra_File.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class ImageAppService(IImageService _imgsrv
       
        ,ILogger<ImageAppService> logger):IImageAppService
    {
        public async Task<bool> DeleteImage(int imageId, string imagePath,CancellationToken ct)
        {
          
            return await _imgsrv.DeleteImage(imageId, ct);
        }
    }
}
