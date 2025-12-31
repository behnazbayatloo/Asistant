using Asistant_Domain_Core.ImageAgg.Data;
using Asistant_Domain_Core.ImageAgg.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Service
{
    public class ImageService(IImageRepository _imgrepo,ILogger<ImageService> logger):IImageService
    {
    }
}
