using Microsoft.AspNetCore.Http;
using Serveo.Application.Abstractions;
using System.Security.Claims;

namespace Serveo.Infrastructure.Authentication
{
    public sealed class HttpRequestContext(IHttpContextAccessor accessor) : IRequestContext
    {
        private HttpContext? HttpContext => accessor.HttpContext;

        public bool IsAuthenticated =>
            HttpContext?.User.Identity?.IsAuthenticated == true;

        public Guid? UserId
        {
            get
            {
                var value = HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(value, out var id)
                    ? id
                    : null;
            }
        }

        public Guid? TenantId
        {
            get
            {
                var value = HttpContext?.User.FindFirst("tenant_id")?.Value;

                return Guid.TryParse(value, out var id)
                    ? id
                    : null;
            }
        }

        public string? UserName =>
            HttpContext?.User.Identity?.Name;

        public string? ClientId =>
            HttpContext?.User.FindFirst("client_id")?.Value;

        public string? IpAddress =>
            HttpContext?.Connection.RemoteIpAddress?.ToString();

        public string? UserAgent =>
            HttpContext?.Request.Headers["User-Agent"].ToString();

        public string TraceId =>
            HttpContext?.TraceIdentifier ?? string.Empty;

        public string? CorrelationId =>
            HttpContext?.Request.Headers["X-Correlation-Id"].FirstOrDefault();
    }
}
