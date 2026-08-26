using Microsoft.AspNetCore.Authorization;
using Serveo.Domain.Interfaces;
using Serveo.Infrastructure.Authentication.Permissions;

namespace Serveo.WebApi.Handlers.AuthorizationHandlers
{
    public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (!context.User.Identity?.IsAuthenticated == true)
                return;

            // Resolve scoped dependencies within a dedicated request scope
            using var scope = _serviceScopeFactory.CreateScope();

            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var sessionContext = scope.ServiceProvider.GetRequiredService<ISessionContext>();

            var userId = sessionContext.UserId;
            var tenantId = sessionContext.TenantId;

            var allowed =
                await permissionService.HasPermissionAsync(
                    userId ?? Guid.Empty,
                    tenantId ?? Guid.Empty,
                    requirement.Permission);

            if (allowed)
            {
                context.Succeed(requirement);
            }
        }
    }
}
