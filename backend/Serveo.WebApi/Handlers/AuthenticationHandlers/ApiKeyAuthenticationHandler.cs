using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Serveo.Application.Abstractions;
using Serveo.Infrastructure.Authentication.ApiKey;
using Serveo.WebApi.Common;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Serveo.WebApi.Handlers.AuthenticationHandlers
{
    // https://chatgpt.com/g/g-p-6a29780664648191a9f08f561a8183e2/c/6a3aa879-5c08-83ec-9d3c-3d5f6c7549a7
    // https://chatgpt.com/c/6a495dde-a998-83ec-b333-01b7aad93e62
    //public class ApiKeyAuthenticationHandler(
    //    IOptions<ApiKeyAuthSettings> settings,
    //    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    //    ILoggerFactory logger,
    //    UrlEncoder encoder) : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
    //{
    //    private readonly IOptions<ApiKeyAuthSettings> _settings = settings;
    //    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    //    {
    //        if (!Request.Headers.TryGetValue(Options.HeaderName, out var apiKey))
    //        {
    //            return Task.FromResult(AuthenticateResult.NoResult());
    //        }

    //        var keyHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(apiKey!)));
    //        if (CryptographicOperations.FixedTimeEquals(
    //            Encoding.UTF8.GetBytes(keyHash),
    //            Encoding.UTF8.GetBytes(_settings.Value.Key)))
    //            return Task.FromResult(AuthenticateResult.Fail("Invalid key."));

    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, "api-client"),
    //            new Claim(ClaimTypes.Name, "API Client"),
    //            //new Claim("OptionsHeaderName", Options.HeaderName),
    //        };

    //        var identity = new ClaimsIdentity(claims, Scheme.Name);
    //        var principal = new ClaimsPrincipal(identity);
    //        var ticket = new AuthenticationTicket(principal, Scheme.Name);

    //        return Task.FromResult(AuthenticateResult.Success(ticket));
    //    }

    //    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    //    {
    //        Response.StatusCode = StatusCodes.Status401Unauthorized;
    //        Response.ContentType = "application/json";

    //        await Response.WriteAsJsonAsync(ApiResponse.Fail(["Authentication is required."]));
    //    }

    //    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    //    {
    //        Response.StatusCode = StatusCodes.Status403Forbidden;
    //        Response.ContentType = "application/json";

    //        await Response.WriteAsJsonAsync(ApiResponse.Fail(["You do not have permission to access this resource."]));
    //    }
    //}

    public sealed class ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IOptions<ApiKeyOptions> apiKeyOptions,
        IApiKeyValidator apiKeyValidator) : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
    {
        private readonly ApiKeyOptions _apiKeyOptions = apiKeyOptions.Value;
        private readonly IApiKeyValidator _apiKeyValidator = apiKeyValidator;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(_apiKeyOptions.HeaderName, out var values))
                return AuthenticateResult.Fail("Missing API key.");

            var apiKey = values.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(apiKey))
                return AuthenticateResult.Fail("Missing API key.");

            var result = await _apiKeyValidator.ValidateAsync(apiKey, Context.RequestAborted);
            if (!result.Succeeded)
                return AuthenticateResult.Fail(result.FailureReason ?? "Invalid API key.");

            var claims = new List<Claim>
            {
                new("client_id", result.ClientId.ToString() ?? string.Empty)
            };

            if (result.TenantId.HasValue)
                claims.Add(new Claim("client_tenant_id", result.TenantId.Value.ToString()));

            var identity = new ClaimsIdentity(claims, ApiKeyAuthenticationOptions.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, ApiKeyAuthenticationOptions.AuthenticationScheme);

            return AuthenticateResult.Success(ticket);
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var statusCode = StatusCodes.Status401Unauthorized;
            var failureReason = properties?.Items.TryGetValue("FailureReason", out var reason) == true ? reason : "UNKNOWN";

            var problem = new ApiProblemDetails
            {
                Type = ProblemTypeCatalog.FromStatusCode(statusCode),
                Title = ProblemTitleCatalog.FromStatusCode(statusCode),
                Status = statusCode,
                Detail = "Authentication is required.",
                Instance = $"{Response.HttpContext.Request.Method} {Response.HttpContext.Request.Path}",
                TraceId = Response.HttpContext.TraceIdentifier,
                Errors =
                [
                    new() { Code = failureReason!, Message = failureReason == "MISSING_API_KEY" ? "Missing API key." : "Invalid API key." }
                ]
            };

            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";
            await Response.WriteAsJsonAsync(problem);
        }

        //protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        //{
        //    var statusCode = StatusCodes.Status401Unauthorized;
        //    var problem = new ApiProblemDetails
        //    {
        //        Type = ProblemTypeCatalog.FromStatusCode(statusCode),
        //        Title = ProblemTitleCatalog.FromStatusCode(statusCode),
        //        Status = statusCode,
        //        Detail = "Authentication is required.",
        //        Instance = $"{Response.HttpContext.Request.Method} {Response.HttpContext.Request.Path}",
        //        TraceId = Response.HttpContext.TraceIdentifier,
        //        Errors =
        //        [
        //            new() {Code = "MISSING_API_KEY", Message = "Missing API key."}
        //        ]
        //    };

        //    Response.StatusCode = statusCode;
        //    Response.ContentType = "application/json";

        //    await Response.WriteAsJsonAsync(problem);
        //}

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            var statusCode = StatusCodes.Status403Forbidden;
            var problem = new ApiProblemDetails
            {
                Type = ProblemTypeCatalog.FromStatusCode(statusCode),
                Title = ProblemTitleCatalog.FromStatusCode(statusCode),
                Status = statusCode,
                Detail = "Authentication is required.",
                Instance = $"{Response.HttpContext.Request.Method} {Response.HttpContext.Request.Path}",
                TraceId = Response.HttpContext.TraceIdentifier,
                Errors =
                [
                    new() {Code = "MISSING_API_KEY", Message = "You do not have permission to access this resource."}
                ]
            };

            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";

            await Response.WriteAsJsonAsync(problem);
        }
    }
}
