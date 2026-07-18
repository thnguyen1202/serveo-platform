using Microsoft.AspNetCore.Http;
using Serveo.Domain;
using Serveo.Domain.Interfaces;
using System.Security.Claims;

namespace Serveo.Infrastructure.Persistence.UnitOfWorks
{
    public sealed class SessionContext(IHttpContextAccessor accessor) : ISessionContext
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private ClaimsPrincipal? User => _accessor.HttpContext?.User;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
        public bool IsSuperAdmin => User?.IsInRole("Super Admin") ?? false;

        public Guid? UserId => TryParseGuid(User?.FindFirstValue(ClaimTypes.NameIdentifier));
        public Guid? TenantId => TryParseGuid(User?.FindFirstValue(CustomClaimTypes.TenantId));
        public Guid? BusinessId => TryParseGuid(User?.FindFirstValue(CustomClaimTypes.BusinessId));
        public Guid? BranchId => TryParseGuid(User?.FindFirstValue(CustomClaimTypes.BranchId));

        private static long? TryParseLong(string? value) => long.TryParse(value, out var v) ? v : null;
        private static int? TryParseInt(string? value) => int.TryParse(value, out var v) ? v : null;
        private static Guid? TryParseGuid(string? value) => Guid.TryParse(value, out var v) ? v : null;
    }
}
