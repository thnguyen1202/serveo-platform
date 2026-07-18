using Serveo.Domain;
using System.Security.Claims;

namespace Serveo.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
        {
            var value = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out userId);
        }

        public static bool TryGetTenantId(this ClaimsPrincipal user, out Guid? tenantId)
        {
            tenantId = null;

            var value = user.FindFirstValue(CustomClaimTypes.TenantId);
            if (string.IsNullOrWhiteSpace(value)) return false;

            if (Guid.TryParse(value, out var outId))
            {
                tenantId = outId;
                return true;
            }

            return false;
        }

        public static bool TryGetBusinessId(this ClaimsPrincipal user, out Guid? businessId)
        {
            businessId = null;

            var value = user.FindFirstValue(CustomClaimTypes.BusinessId);
            if (string.IsNullOrWhiteSpace(value)) return false;

            if (Guid.TryParse(value, out var outId))
            {
                businessId = outId;
                return true;
            }

            return false;
        }

        public static bool TryGetBranchId(this ClaimsPrincipal user, out Guid? branchId)
        {
            branchId = null;

            var value = user.FindFirstValue(CustomClaimTypes.BranchId);
            if (string.IsNullOrWhiteSpace(value)) return false;

            if (Guid.TryParse(value, out var outId))
            {
                branchId = outId;
                return true;
            }

            return false;
        }
    }
}
