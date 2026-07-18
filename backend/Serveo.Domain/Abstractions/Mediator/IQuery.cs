namespace Serveo.Domain.Abstractions.Mediator
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
        string? CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
