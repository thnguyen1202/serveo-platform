namespace Serveo.Infrastructure.Authentication.ApiKey
{
    public class ApiKeyOptions
    {
        public const string SectionName = "Authentication:Schemes:ApiKey";

        public string HeaderName { get; init; } = "X-API-KEY";
        public bool RequireForLogin { get; init; } = true;
        public bool RequireForRefresh { get; init; } = true;
    }
}
