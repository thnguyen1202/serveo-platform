namespace Serveo.Application.Abstractions.Messaging
{
    public interface ICacheable
    {
        string CacheKey { get; }
        TimeSpan Expiration { get; }
    }
}
