using Serveo.Application.Common.Results;

namespace Serveo.WebApi.Common
{
    // Problem type mapper
    // Để field type trong ProblemDetails đẹp và ổn định, map theo status/error type.
    public static class ProblemTypeCatalog
    {
        public static string FromErrors(IReadOnlyCollection<ICommandError> errors)
        {
            if (errors.Count == 0)
                return "https://api.serveo.app/problems/internal-server-error";

            if (errors.Any(e => e.Type == ErrorType.Validation))
                return "https://api.serveo.app/problems/validation";

            if (errors.Any(e => e.Type == ErrorType.Unauthorized))
                return "https://api.serveo.app/problems/unauthorized";

            if (errors.Any(e => e.Type == ErrorType.Forbidden))
                return "https://api.serveo.app/problems/forbidden";

            if (errors.Any(e => e.Type == ErrorType.NotFound))
                return "https://api.serveo.app/problems/not-found";

            if (errors.Any(e => e.Type == ErrorType.Conflict))
                return "https://api.serveo.app/problems/conflict";

            if (errors.Any(e => e.Type == ErrorType.Business))
                return "https://api.serveo.app/problems/business";

            return "https://api.serveo.app/problems/internal-server-error";
        }

        public static string FromStatusCode(int statusCode) => statusCode switch
        {
            //StatusCodes.Status400BadRequest => "https://api.serveo.app/problems/validation",
            //StatusCodes.Status401Unauthorized => "https://api.serveo.app/problems/unauthorized",
            //StatusCodes.Status403Forbidden => "https://api.serveo.app/problems/forbidden",
            //StatusCodes.Status404NotFound => "https://api.serveo.app/problems/not-found",
            //StatusCodes.Status409Conflict => "https://api.serveo.app/problems/conflict",
            //StatusCodes.Status422UnprocessableEntity => "https://api.serveo.app/problems/business",
            //_ => "https://api.serveo.app/problems/internal-server-error"

            StatusCodes.Status400BadRequest => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
            StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.2",
            StatusCodes.Status403Forbidden => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.4",
            StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.5",
            StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.10",
            StatusCodes.Status422UnprocessableEntity => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.21",
            _ => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1"
        };
    }
}
