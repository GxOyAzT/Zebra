using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.API.Middlewares
{
    public class CatchExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CatchExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiBaseException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }

    public static class CatchExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCatchExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CatchExceptionMiddleware>();
        }
    }
}
