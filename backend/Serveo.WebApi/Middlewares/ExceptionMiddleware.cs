using Serveo.WebApi.Common;
using System.Diagnostics;

namespace Serveo.WebApi.Middlewares
{
    // ExceptionMiddleware cho 500
    public sealed class ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unhandled exception.RequestId={RequestId}, TraceId={TraceId}, SpanId={SpanId}, Path={Path}",
                    context.TraceIdentifier,
                    Activity.Current?.TraceId.ToString(),
                    Activity.Current?.SpanId.ToString(),
                    context.Request.Path);

                await WriteProblemDetailsAsync(context, ex);
            }
        }

        private static async Task WriteProblemDetailsAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var errors = new List<ApiProblemError>() {
                new() { Code = "EXCEPTION_PROBLEM", Message = ex.Message, Type = "Exception" },
            };
            var inner = ex.InnerException;
            while (inner != null)
            {
                errors.Add(new() { Code = "Exception", Message = inner.Message, Type = "Exception" });
                inner = inner.InnerException;
            }

            var problem = new ApiProblemDetails
            {
                Type = ProblemTypeCatalog.FromStatusCode(StatusCodes.Status500InternalServerError),
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An unexpected error occurred while processing the request.",
                Instance = $"{context.Request.Method} {context.Request.Path}",
                TraceId = Activity.Current?.Id,
                Errors = errors
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
