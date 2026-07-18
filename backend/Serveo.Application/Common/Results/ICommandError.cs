namespace Serveo.Application.Common.Results
{
    public interface ICommandError
    {
        string Code { get; }
        string Message { get; }
        ErrorType Type { get; }
        string? Target { get; }
    }


}
