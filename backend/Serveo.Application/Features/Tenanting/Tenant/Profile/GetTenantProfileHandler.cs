using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Common.Results;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Tenanting.Tenant.Profile
{
    public sealed class GetTenantProfileHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<GetTenantProfileCommand, ICommandResult<TenantDto>>
    {
        public async Task<ICommandResult<TenantDto>> HandleAsync(GetTenantProfileCommand request, CancellationToken ct)
        {
            var keys = new object[] { request.Id };
            var entity = await unitOfWork.Tenants.FindAsync(keys, ct);

            if (entity == null)
            {
                return CommandResult<TenantDto>.Failure(
                    CommandErrors.NotFound(ErrorCodes.Tenant.NotFound, "Tenant not found."));
            }

            return CommandResult<TenantDto>.Success(mapper.Map<TenantDto>(entity));
        }
    }
}
