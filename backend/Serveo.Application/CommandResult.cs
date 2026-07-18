using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Common.Results;

namespace Serveo.Application.Services
{
    public class CommandResult : ICommandResult
    {
        public bool Succeeded { get; init; }
        public IReadOnlyList<ICommandError> Errors { get; init; } = [];


        private CommandResult(bool succeeded, IReadOnlyList<ICommandError> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }


        public static CommandResult Success() => new(true, []);
        public static CommandResult Failure(params ICommandError[] errors) => new(false, errors);
        public static CommandResult Failure(IEnumerable<ICommandError> errors) => new(false, [.. errors]);
    }

    public sealed class CommandResult<T> : ICommandResult<T>
    {
        public bool Succeeded { get; init; }
        public IReadOnlyList<ICommandError> Errors { get; init; }
        public T Value { get; init; }


        private CommandResult(T value)
        {
            Succeeded = true;
            Value = value;
            Errors = [];
        }

        private CommandResult(IReadOnlyList<ICommandError> errors)
        {
            Succeeded = false;
            Errors = errors;
            Value = default!;
        }


        public static CommandResult<T> Success(T value) => new(value);
        public static CommandResult<T> Failure(params ICommandError[] errors) => new(errors);
        public static CommandResult<T> Failure(IEnumerable<ICommandError> errors) => new([.. errors]);

        //public static ICommandResult<T> Success(T data) => new CommandResult<T>
        //{
        //    Succeeded = true,
        //    Value = data
        //};

        //public static new ICommandResult<T> Failure(params ICommandError[] errors) => new CommandResult<T>
        //{
        //    Succeeded = false,
        //    Errors = errors
        //};

        //public static new ICommandResult<T> Failure(IEnumerable<ICommandError> errors) => new CommandResult<T>
        //{
        //    Succeeded = false,
        //    Errors = errors
        //};
    }

    //public static class CommandResult
    //{
    //    public static ICommandResult Success()
    //        => new Result { Succeeded = true };

    //    public static ICommandResult<T> Success<T>(T data)
    //        => new Result<T>
    //        {
    //            Succeeded = true,
    //            Data = data
    //        };

    //    public static ICommandResult Failure(params ICommandError[] errors)
    //        => new Result
    //        {
    //            Succeeded = false,
    //            Errors = errors
    //        };
    //}

    //public class CommandResult: CommandResult<object>
    //{
    //    public static new CommandResult Success => (CommandResult)_success;

    //    public static new CommandResult Failed(params CommandError[] errors)
    //    {
    //        var result = new CommandResult { Succeeded = false };
    //        if (errors != null)
    //        {
    //            result._errors.AddRange(errors);
    //        }
    //        return result;
    //    }

    //    public static new CommandResult Failed(List<CommandError>? errors)
    //    {
    //        var result = new CommandResult { Succeeded = false };
    //        if (errors != null)
    //        {
    //            result._errors.AddRange(errors);
    //        }
    //        return result;
    //    }
    //}

    //public class CommandResult<T> : ICommandResult<T>
    //{
    //    protected static readonly CommandResult<T> _success = new() { Succeeded = true };
    //    protected readonly List<CommandError> _errors = [];

    //    public bool Succeeded { get; protected set; }
    //    public IEnumerable<ICommandError> Errors => _errors;
    //    public T? Data { get; set; }

    //    public static CommandResult<T> Success => _success;

    //    public static CommandResult<T> Failed(params CommandError[] errors)
    //    {
    //        var result = new CommandResult<T> { Succeeded = false };
    //        if (errors != null)
    //        {
    //            result._errors.AddRange(errors);
    //        }
    //        return result;
    //    }

    //    public static CommandResult<T> Failed(List<CommandError>? errors)
    //    {
    //        var result = new CommandResult<T> { Succeeded = false };
    //        if (errors != null)
    //        {
    //            result._errors.AddRange(errors);
    //        }
    //        return result;
    //    }

    //    public static CommandResult<T> Failed(IEnumerable<IdentityError> errors)
    //    {
    //        var result = new CommandResult<T> { Succeeded = false };
    //        if (errors != null)
    //        {
    //            var commandErrors = errors.Select(s => new CommandError(s)).ToList();
    //            result._errors.AddRange(commandErrors);
    //        }
    //        return result;
    //    }
    //}
}
