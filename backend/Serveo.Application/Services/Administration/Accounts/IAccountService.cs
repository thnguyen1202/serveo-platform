using Serveo.Application.Abstractions.Mediator;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Application.Services.Administration.Accounts
{
    public interface IAccountService
    {
        Task<ICommandResult<User>> LoginAsync(string emailOrUsername, string password);
    }
}