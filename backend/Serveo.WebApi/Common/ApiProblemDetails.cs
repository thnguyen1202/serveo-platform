using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Serveo.WebApi.Common
{
    // Model response cho API error
    public sealed class ApiProblemDetails : ProblemDetails
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TraceId { get; init; }

        public List<ApiProblemError> Errors { get; init; } = [];
    }

    public sealed class ApiProblemError
    {
        public string Code { get; init; } = default!;
        public string Message { get; init; } = default!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; init; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; init; }
    }
}
