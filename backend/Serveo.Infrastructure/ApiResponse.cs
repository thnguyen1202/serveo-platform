namespace Serveo.Infrastructure
{
    //public class ApiResponse
    //{
    //    public static ApiResponse<T> Ok<T>(T data, string? message = null) => new()
    //    {
    //        Success = true,
    //        Data = data,
    //        Message = message
    //    };

    //    public static ApiResponse<T> Fail<T>(IEnumerable<string> errors, string? message = null) => new()
    //    {
    //        Success = false,
    //        Message = message,
    //        Errors = [.. errors],
    //    };
    //}

    public class ApiResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public List<string>? Errors { get; init; }
        public string? TraceId { get; init; }

        public static ApiResponse Ok(string? message = null) => new()
        {
            Success = true,
            Message = message
        };

        public static ApiResponse Fail(IEnumerable<string> errors, string? message = null) => new()
        {
            Success = false,
            Message = message,
            Errors = [.. errors]
        };
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; init; }

        public static ApiResponse<T> Ok(T data, string? message = null) => new()
        {
            Success = true,
            Data = data,
            Message = message
        };


        //public static ApiResponse<T> Ok(T data, string? message = null) => new()
        //{
        //    Success = true,
        //    Data = data,
        //    Message = message
        //};

        //public static ApiResponse<T> Fail(IEnumerable<string> errors, string? message = null) => new()
        //{
        //    Success = false,
        //    Message = message,
        //    Errors = [.. errors]
        //};
    }
}
