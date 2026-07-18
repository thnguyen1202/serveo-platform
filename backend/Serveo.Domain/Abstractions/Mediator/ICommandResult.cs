namespace Serveo.Domain.Abstractions.Mediator
{
    public interface ICommandResult
    {
        bool Succeeded { get; }
        IEnumerable<ICommandError> Errors { get; }
    }

    public interface ICommandResult<out T> : ICommandResult
    {
        T? Data { get; }
    }
}
