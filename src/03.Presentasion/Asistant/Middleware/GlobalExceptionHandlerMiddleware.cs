
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Asistant.Middleware
{

    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next; 
        public GlobalExceptionHandlerMiddleware(RequestDelegate next) 
        { 
            _next = next;
        }
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
            Log.Error(exception, "خطای غیرمنتظره در پردازش درخواست رخ داد");
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
