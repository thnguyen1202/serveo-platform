using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Domain.Interfaces;

namespace Serveo.Infrastructure.Services
{
    public class BranchAccessService(IUnitOfWork unitOfWork, ISessionContext context) : IBranchAccessService
    {
        public async Task<bool> HasAccessAsync(
            Guid branchId,
            CancellationToken cancellationToken)
        {
            var userId = context.UserId;
            var tenantId = context.TenantId;

            return await unitOfWork.TenantMembers.Query()
                .Where(x =>
                    x.UserId == userId &&
                    x.TenantId == tenantId)
                .AnyAsync(
                    x =>
                        x.IsAllBranches ||
                        x.TenantMemberBranches.Any(b =>
                            b.BranchId == branchId),
                    cancellationToken);
        }
    }
}
