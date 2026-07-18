using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Common.Results;
using System.Diagnostics;

namespace Serveo.WebApi.Common
{
    // Extension map từ ICommandResult sang IActionResult
    public static class CommandResultMvcExtensions
    {
        public static IActionResult ToActionResult(
            this ControllerBase controller,
            ICommandResult result)
        {
            if (result.Succeeded)
                return controller.NoContent();

            var problem = CreateProblemDetails(
                controller.HttpContext,
                [.. result.Errors]);

            return controller.StatusCode(problem.Status ?? 500, problem);
        }

        public static IActionResult ToActionResult<T>(
            this ControllerBase controller,
            ICommandResult<T> result,
            Func<T, IActionResult>? onSuccess = null)
        {
            if (result.Succeeded)
            {
                if (onSuccess is not null)
                    return onSuccess(result.Value);

                return controller.Ok(result.Value);
            }

            var problem = CreateProblemDetails(
                controller.HttpContext,
                [.. result.Errors]);

            return controller.StatusCode(problem.Status ?? 500, problem);
        }

        //public static IActionResult ToActionResult<TSource, TResponse>(
        //    this ControllerBase controller,
        //    ICommandResult<TSource> result,
        //    Func<TSource, TResponse> selector)
        //{
        //    if (!result.Succeeded)
        //        return controller.ToActionResult(result);

        //    return controller.Ok(selector(result.Value));
        //}

        private static ApiProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            IReadOnlyCollection<ICommandError> errors)
        {
            var statusCode = CommandErrorHttpMapper.MapStatusCode(errors);

            var detail = BuildDetail(statusCode, errors);

            return new ApiProblemDetails
            {
                Type = ProblemTypeCatalog.FromStatusCode(statusCode),
                Title = ProblemTitleCatalog.FromStatusCode(statusCode),
                Status = statusCode,
                Detail = detail,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                TraceId = Activity.Current?.Id, // Activity.Current?.Id - Activity.Current?.TraceId.ToString()
                //TraceId = httpContext.TraceIdentifier,
                //RequestId = httpContext.TraceIdentifier,
                Errors = [.. errors.Select(e => new ApiProblemError
                {
                    Code = e.Code,
                    Message = e.Message,
                    Type = e.Type.ToString(),
                    Target = e.Target
                })]
            };
        }

        private static string BuildDetail(int statusCode, IReadOnlyCollection<ICommandError> errors)
        {
            if (errors.Count == 0)
                return "An unexpected error occurred.";

            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "The request is invalid.",
                StatusCodes.Status401Unauthorized => "Authentication is required or has failed.",
                StatusCodes.Status403Forbidden => "You do not have permission to perform this action.",
                StatusCodes.Status404NotFound => "The requested resource was not found.",
                StatusCodes.Status409Conflict => "The request conflicts with the current state of the resource.",
                StatusCodes.Status422UnprocessableEntity => "The requested operation violates a business rule.",
                _ => "An unexpected error occurred."
            };
        }
    }
}
