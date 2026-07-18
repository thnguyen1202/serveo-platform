using Serveo.Domain.Exceptions;
using Serveo.Infrastructure;

namespace Serveo.WebApi.Handlers.Middleware
{
    public class ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = context.TraceIdentifier;

            try
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["X-Trace-Id"] = traceId;
                    return Task.CompletedTask;
                });

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex,
            string traceId)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message, errors) = MapException(ex);

            context.Response.StatusCode = statusCode;

            _logger.LogError(ex,
                "Unhandled exception. TraceId={TraceId}, Path={Path}, Method={Method}",
                traceId,
                context.Request.Path,
                context.Request.Method);

            var response = new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Errors = errors,
                TraceId = traceId
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private static (int statusCode, string message, List<string> errors)
            MapException(Exception ex)
        {
            return ex switch
            {
                BusinessException be => (
                    StatusCodes.Status400BadRequest,
                    be.Message,
                    new List<string> { be.Code }
                ),

                KeyNotFoundException => (
                    StatusCodes.Status404NotFound,
                    "Resource not found",
                    new List<string>()
                ),

                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    new List<string>()
                ),

                TimeoutException => (
                    StatusCodes.Status504GatewayTimeout,
                    "External service timeout",
                    new List<string> { ex.Message }
                ),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Internal server error",
                    new List<string>()
                )
            };
        }
    }
}
