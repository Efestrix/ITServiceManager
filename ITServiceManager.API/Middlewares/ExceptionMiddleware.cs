using ITServiceManager.API.Entities;
using System.Text.Json;

namespace ITServiceManager.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,

                NotFoundException => StatusCodes.Status404NotFound,

                UnauthorizedException => StatusCodes.Status401Unauthorized,

                ForbiddenException => StatusCodes.Status403Forbidden,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "applcation/json";

            context.Response.StatusCode = statusCode;

            ErrorResponse response = new()
            {
                StatusCode = statusCode,
                Message = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
