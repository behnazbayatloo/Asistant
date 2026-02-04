using Asistant_Domain_Core._commonEntities;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UserAgg.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_AppService
{
    public class CityAppService(ICityService _citysrv,ILogger<CityAppService> logger):ICityAppService
    {
        public async Task<List<City>> GetAllCities(CancellationToken ct)=> await _citysrv.GetAllCities(ct);
    public async Task<PagedResult<CityDTO>> GetPagedCities(int pageNumber,int pageSize,CancellationToken ct)
            => await _citysrv.GetPagedCities(pageNumber, pageSize, ct);
        public async Task<bool> DeleteCity(int id,CancellationToken ct)
            => await _citysrv.DeleteCity(id, ct);
        public async Task<Result<bool>> CreateCity(CityDTO cityDTO, CancellationToken ct)
        {
            var exist = await _citysrv.ExistCity(cityDTO.CityName, ct);
           
            if (exist)
            {
                return Result<bool>.Failure("این شهر در لیست موجود است"); }
            else
            {
               var result= await _citysrv.CreateCity(cityDTO, ct);
                if(!result)
                {
                    return Result<bool>.Failure("مشکلی در اضافه کردن شهر وجود دارد ");
                }
                else
                {
                    return Result<bool>.Success(result, "شهر با موفقیت اضافه شد");
                }
                    
            }
        }
    }
}
