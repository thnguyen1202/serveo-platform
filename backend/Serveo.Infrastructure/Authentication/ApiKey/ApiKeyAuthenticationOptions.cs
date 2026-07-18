using Microsoft.AspNetCore.Authentication;

namespace Serveo.Infrastructure.Authentication.ApiKey
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string AuthenticationScheme = "ApiKey";
    }
}
