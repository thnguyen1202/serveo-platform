using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Ordering;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Features.Ordering.QR
{
    public sealed class ScanHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<ScanCommand, ScanResult>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ScanResult> HandleAsync(ScanCommand request, CancellationToken ct)
        {
            var table = await _unitOfWork.Tables.Query()
                //.Include(i => i.Branch)
                //.ThenInclude(i => i.Business)
                .FirstOrDefaultAsync(x => x.PublicToken == request.Token, cancellationToken: ct);

            return new ScanResult { Table = _mapper.Map<TableDto>(table) };
        }
    }
}
