namespace Serveo.Application.Abstractions
{
    public interface IRequestContext
    {
        //HttpContext HttpContext { get; }

        Guid? UserId { get; }
        Guid? TenantId { get; }
        string? UserName { get; }
        string? ClientId { get; }
        string? IpAddress { get; }
        string? UserAgent { get; }
        string TraceId { get; }
        string? CorrelationId { get; }
        bool IsAuthenticated { get; }
    }
}
