using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Tekus.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Mapeo de Excepciones a Códigos HTTP
            context.Response.StatusCode = exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest, // 400
                KeyNotFoundException => (int)HttpStatusCode.NotFound,  // 404
                ArgumentException => (int)HttpStatusCode.BadRequest,   // 400
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401
                _ => (int)HttpStatusCode.InternalServerError // 500
            };

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Errors = (exception as ValidationException)?.Errors.Select(e => e.ErrorMessage)
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
