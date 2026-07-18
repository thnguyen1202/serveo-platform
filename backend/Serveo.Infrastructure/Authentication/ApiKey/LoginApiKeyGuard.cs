using Serveo.Application.Abstractions;

namespace Serveo.Infrastructure.Authentication.ApiKey
{
    // Protect login with API key without forcing JWT
    // This is my preferred approach for login endpoints because login is [AllowAnonymous] from the user perspective, but still gated by client API key.
    // This is cleaner than trying to force [Authorize(AuthenticationSchemes = "ApiKey")] on the login endpoint while also keeping it “anonymous” from a user auth standpoint.
    //public sealed class LoginApiKeyGuard(
    //    IAuthenticationService authenticationService,
    //    IOptions<ApiKeyOptions> options) : ILoginApiKeyGuard
    //{
    //    private readonly ApiKeyOptions _options = options.Value;

    //    public async Task EnsureValidAsync(HttpContext httpContext, CancellationToken ct)
    //    {
    //        if (!_options.RequireForLogin)
    //            return;

    //        var authResult = await authenticationService.AuthenticateAsync(
    //            httpContext,
    //            ApiKeyAuthenticationOptions.AuthenticationScheme);

    //        if (!authResult.Succeeded)
    //            throw new UnauthorizedAccessException("Invalid API key.");

    //        //if (!result.Succeeded || result.Principal is null)
    //        //    throw new UnauthorizedAccessException("Invalid API key.");

    //        //var clientId = result.Principal.FindFirst("client_id")?.Value
    //        //    ?? throw new UnauthorizedAccessException("Invalid API key.");

    //        //var tenantIdRaw = result.Principal.FindFirst("client_tenant_id")?.Value;
    //        //long? tenantId = long.TryParse(tenantIdRaw, out var parsed) ? parsed : null;

    //        //return new LoginClientContext(clientId, tenantId);
    //    }
    //}
}
