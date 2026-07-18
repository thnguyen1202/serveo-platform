namespace Serveo.Application.Common.Results
{
    public sealed record CommandError(
        string Code,
        string Message,
        ErrorType Type,
        string? Target = null
    ) : ICommandError;
}
