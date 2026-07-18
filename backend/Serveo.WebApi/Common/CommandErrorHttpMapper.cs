using Serveo.Application.Common.Results;

namespace Serveo.WebApi.Common
{
    // Status code mapper
    // Tách riêng chỗ map ErrorType → HTTP status.
    public static class CommandErrorHttpMapper
    {
        public static int MapStatusCode(IReadOnlyCollection<ICommandError> errors)
        {
            if (errors.Count == 0)
                return StatusCodes.Status500InternalServerError;

            // Nếu có nhiều loại lỗi cùng lúc, ưu tiên theo mức nghiêm trọng/ngữ nghĩa
            if (errors.Any(e => e.Type == ErrorType.Unauthorized))
                return StatusCodes.Status401Unauthorized;

            if (errors.Any(e => e.Type == ErrorType.Forbidden))
                return StatusCodes.Status403Forbidden;

            if (errors.Any(e => e.Type == ErrorType.NotFound))
                return StatusCodes.Status404NotFound;

            if (errors.Any(e => e.Type == ErrorType.Conflict))
                return StatusCodes.Status409Conflict;

            if (errors.Any(e => e.Type == ErrorType.Business))
                return StatusCodes.Status422UnprocessableEntity;

            if (errors.Any(e => e.Type == ErrorType.Validation))
                return StatusCodes.Status400BadRequest;

            return StatusCodes.Status500InternalServerError;
        }
    }
}
