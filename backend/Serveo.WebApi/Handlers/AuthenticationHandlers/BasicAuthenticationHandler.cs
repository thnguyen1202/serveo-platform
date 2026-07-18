using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Serveo.Infrastructure;
using Serveo.WebApi.Appsettings;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Serveo.WebApi.Handlers.AuthenticationHandlers
{
    public class BasicAuthenticationHandler(
        IOptions<BasicAuthSettings> settings,
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : AuthenticationHandler<BasicAuthenticationOptions>(options, logger, encoder)
    {
        private readonly IOptions<BasicAuthSettings> _settings = settings;
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
                return Task.FromResult(AuthenticateResult.NoResult());

            if (!authHeader.ToString().StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.NoResult());

            string username;
            string password;

            try
            {
                var encoded = authHeader.ToString()["Basic ".Length..].Trim();
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
                var parts = decoded.Split(':', 2);

                if (parts.Length != 2)
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header."));

                username = parts[0];
                password = parts[1];
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header."));
            }

            if (username != _settings.Value.Username || password != _settings.Value.Password)
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            Response.Headers.WWWAuthenticate = "Basic realm=\"api\"";

            await Response.WriteAsJsonAsync(ApiResponse.Fail(["Basic authentication required."], "Unauthorized"));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = StatusCodes.Status403Forbidden;

            await Response.WriteAsJsonAsync(ApiResponse.Fail(["Access denied."], "Forbidden"));
        }
    }
}
