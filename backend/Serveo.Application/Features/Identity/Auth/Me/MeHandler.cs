using AutoMapper;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Common.Results;
using Serveo.Application.Dtos.Identity;
using Serveo.Application.Services;

namespace Serveo.Application.Features.Identity.Auth.Me
{
    public sealed class MeHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : ICommandHandler<MeCommand, ICommandResult<UserDto>>
    {
        public async Task<ICommandResult<UserDto>> HandleAsync(MeCommand request, CancellationToken ct)
        {
            var keys = new object[] { unitOfWork.Session.UserId ?? Guid.Empty };
            var user = await unitOfWork.Users.FindAsync(keys, ct);

            if (user == null)
            {
                return CommandResult<UserDto>.Failure(
                    CommandErrors.NotFound(ErrorCodes.User.NotFound, "User not found."));
            }

            return CommandResult<UserDto>.Success(mapper.Map<UserDto>(user));
        }
    }
}
