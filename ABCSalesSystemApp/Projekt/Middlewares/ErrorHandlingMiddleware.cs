using Projekt.Exceptions;
using System.Net;
using System.Text.Json;

namespace Projekt.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;

            if (exception is UnverifiedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is NotFoundException || exception is NoContentException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = exception.GetType().Name,
                message = exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
