using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Ordering;
using Serveo.SharedKernel;

namespace Serveo.Application.Features.Ordering.Tables.Create
{
    public sealed class CreateTableHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<CreateTableCommand, ICommandResult<CreateTableResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ICommandResult<CreateTableResult>> HandleAsync(CreateTableCommand request, CancellationToken ct)
        {
            var entity = new Table()
            {
                Code = Guid.NewGuid().ToString("N").ToUpper(),
                PublicToken = PublicTokenGenerator.Generate()
            };
            _unitOfWork.SetValues(entity, request);
            await _unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateTableResult>.Success(_mapper.Map<CreateTableResult>(entity));
        }
    }
}
