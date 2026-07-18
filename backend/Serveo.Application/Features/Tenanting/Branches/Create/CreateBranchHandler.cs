using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Application.Features.Tenanting.Branches.Create
{
    public sealed class CreateBranchHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateBranchCommand, ICommandResult<CreateBranchResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateBranchResult>> HandleAsync(CreateBranchCommand request, CancellationToken ct)
        {
            var value = await _unitOfWork.ExecuteAsync(async () =>
            {
                var branch = _mapper.Map<Branch>(request);

                if (!request.BusinessId.HasValue)
                {
                    // create tenant
                    var tenant = new Domain.Entities.Tenanting.Tenant { Name = request.Name };
                    _unitOfWork.Add(tenant);

                    // create business
                    var business = new Business { Name = request.Name, Address = request.Address, TenantId = tenant.Id };
                    _unitOfWork.Add(business);

                    // set BusinessId
                    branch.BusinessId = business.Id;
                }

                _unitOfWork.Add(branch);

                return _mapper.Map<CreateBranchResult>(branch);
            }, ct);

            return CommandResult<CreateBranchResult>.Success(value);
        }
    }
}
