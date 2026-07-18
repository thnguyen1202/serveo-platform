using Microsoft.AspNetCore.Diagnostics;
using Serveo.WebApi.Common;
using System.Diagnostics;

namespace Serveo.WebApi.Handlers.ExceptionHandler
{
    internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception,
                "Unhandled exception. Path={Path}, RequestId={RequestId}, ActivityId={TraceId}",
                httpContext.Request.Path,
                httpContext.TraceIdentifier,
                Activity.Current?.Id
            );

            var errors = new List<ApiProblemError>() {
                new() { Code = "EXCEPTION_PROBLEM", Message = exception.Message, Type = "Exception" },
            };
            var inner = exception.InnerException;
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
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                TraceId = Activity.Current?.Id,
                Errors = errors
            };

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

            return true;
        }
    }
}
