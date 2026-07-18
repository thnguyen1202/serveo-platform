using Serveo.Application.Common.Results;

namespace Serveo.Application.Abstractions.Mediator
{
    public interface ICommandResult
    {
        bool Succeeded { get; }
        IReadOnlyList<ICommandError> Errors { get; }
    }

    public interface ICommandResult<out T> : ICommandResult
    {
        T Value { get; }
    }
}
