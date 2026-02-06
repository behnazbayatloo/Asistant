using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.DTOs;
using Asistant_Domain_Core.UserAgg.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Asistant_Presentasion_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(
        IAppUserAppService appuser
       ) : ControllerBase

    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await appuser.Register(ct, dto);
            if (result.Succeeded)
                return Ok(new { Message = "ثبت‌نام با موفقیت انجام شد" }); 
            


                return BadRequest(result.Errors.Select(e => e.Description));
        
        }
    }
}
