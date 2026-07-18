using Microsoft.AspNetCore.Authentication;

namespace Serveo.WebApi.Handlers.AuthenticationHandlers
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string AuthenticationScheme = "Basic";
    }
}
