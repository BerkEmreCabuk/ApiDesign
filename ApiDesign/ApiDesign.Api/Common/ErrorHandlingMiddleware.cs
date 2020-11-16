using System;
using System.Text.Json;
using System.Threading.Tasks;
using ApiDesign.Api.Models;
using Microsoft.AspNetCore.Http;

namespace ApiDesign.Api.Common
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(
            RequestDelegate next
        )
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/json";
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new BaseResponseModel("Beklenilmeyen bir hata oluştu.")));
            }
        }
    }
}