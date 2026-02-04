
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Asistant.Middleware
{



    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env; 

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
               
                HandleException(context, ex);
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 

          
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                context.Response.ContentType = "application/json";
                context.Response.WriteAsync(new { error = "خطایی رخ داده است" }.ToString());
            }
            else
            {
              
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
