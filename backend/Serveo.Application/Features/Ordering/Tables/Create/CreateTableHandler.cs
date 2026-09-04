using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Ordering;
using Serveo.Application.Services;
using Serveo.Domain.Entities.Ordering;
using Serveo.SharedKernel;

namespace Serveo.Application.Features.Ordering.Tables.Create
{
    public sealed class CreateTableHandler(
        IUnitOfWork unitOfWork,
        CommandMapper mapper
    ) : ICommandHandler<CreateTableCommand, ICommandResult<CreateTableResult>>
    {
        public async Task<ICommandResult<CreateTableResult>> HandleAsync(CreateTableCommand request, CancellationToken ct)
        {
            var entity = new Table()
            {
                Code = Guid.NewGuid().ToString("N").ToUpper(),
                PublicToken = PublicTokenGenerator.Generate()
            };
            unitOfWork.SetValues(entity, request);
            unitOfWork.Add(entity);
            await unitOfWork.SaveChangesAsync(ct);

            return CommandResult<CreateTableResult>.Success(mapper.ToResult(entity));
        }
    }
}
