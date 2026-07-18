namespace Serveo.Application.Abstractions
{
    //public interface ILoginApiKeyGuard
    //{
    //    Task EnsureValidAsync(HttpContext httpContext, CancellationToken ct);
    //}

    public sealed record LoginClientContext(
        string ClientId,
        long? TenantId
    );
}
