using Microsoft.AspNetCore.Authorization;

namespace Serveo.WebApi.Handlers.AuthorizationHandlers
{
    /// <summary>
    /// using : [RequirePermission(Permissions.Orders.Update)]
    /// </summary>
    public sealed class RequirePermissionAttribute
    : AuthorizeAttribute
    {
        public RequirePermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
