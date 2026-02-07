using Newtonsoft.Json;
using System.Net;

namespace Asistant_Presentasion_Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next; public GlobalExceptionHandlerMiddleware(RequestDelegate next) { _next = next; }
        public async Task Invoke(HttpContext context)
        {
            try 
            { 
                await _next(context); 
            }
            catch (TaskCanceledException)
            {  
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            } 
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            } 
        } 
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        { 
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = System.Text.Json.JsonSerializer.Serialize(new 
            {
                error = "خطایی رخ داده است", 
                details = exception.Message 
            });
            await context.Response.WriteAsync(result);
        }
           
    }
}
