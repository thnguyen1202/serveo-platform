namespace Serveo.WebApi.Common
{
    // Title mapper
    public static class ProblemTitleCatalog
    {
        public static string FromStatusCode(int statusCode) => statusCode switch
        {
            StatusCodes.Status400BadRequest => "One or more validation errors occurred.",
            StatusCodes.Status401Unauthorized => "Unauthorized.",
            StatusCodes.Status403Forbidden => "Forbidden.",
            StatusCodes.Status404NotFound => "Resource not found.",
            StatusCodes.Status409Conflict => "Conflict occurred.",
            StatusCodes.Status422UnprocessableEntity => "Business rule violated.",
            _ => "An unexpected error occurred."
        };

        public static string FromStatusCode(int statusCode, string? fallbackDetail)
        {
            if (!string.IsNullOrWhiteSpace(fallbackDetail))
                return fallbackDetail;

            return FromStatusCode(statusCode);
        }
    }
}
