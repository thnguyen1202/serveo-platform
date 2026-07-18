using Microsoft.AspNetCore.Authentication;

namespace Serveo.WebApi.Handlers.AuthenticationHandlers
{
    public sealed class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string SectionName = "Authentication:Schemes:ApiKey";
        public const string AuthenticationScheme = "ApiKey";

        public string HeaderName { get; init; } = "X-API-KEY";
        public bool RequireForLogin { get; init; } = true;
        public bool RequireForRefresh { get; init; } = true;
    }
}
