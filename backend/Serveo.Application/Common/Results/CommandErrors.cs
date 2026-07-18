namespace Serveo.Application.Common.Results
{
    public static class CommandErrors
    {
        public static CommandError Validation(
            string code,
            string message,
            string? target = null)
            => new(code, message, ErrorType.Validation, target);

        public static CommandError NotFound(
            string code,
            string message,
            string? target = null)
            => new(code, message, ErrorType.NotFound, target);

        public static CommandError Conflict(
            string code,
            string message,
            string? target = null)
            => new(code, message, ErrorType.Conflict, target);

        public static CommandError Unauthorized(
            string code,
            string message,
            string? target = null)
            => new(code, message, ErrorType.Unauthorized, target);

        public static CommandError Forbidden(
            string code,
            string message,
            string? target = null)
            => new(code, message, ErrorType.Forbidden, target);

        public static CommandError Business(
            string code,
            string message,
            string? target = null)
            => new(code, message, ErrorType.Business, target);
    }
}
