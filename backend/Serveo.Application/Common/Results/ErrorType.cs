namespace Serveo.Application.Common.Results
{
    public enum ErrorType : byte
    {
        // 400 hoặc 422: request sai, field sai, business validation fail
        Validation = 1,

        // 404: không tìm thấy resource
        NotFound,

        // 409: email đã tồn tại, state conflict, concurrency
        Conflict,

        // 403: có identity nhưng không có quyền
        Forbidden,

        // 401: chưa xác thực / token sai / login fail
        Unauthorized,

        // 422: rule nghiệp vụ không cho phép nhưng không phải validation syntax
        Business,

        // 500: lỗi hệ thống thật
        // Exception
    }
}
