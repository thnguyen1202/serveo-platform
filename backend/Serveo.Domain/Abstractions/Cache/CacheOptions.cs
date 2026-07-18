namespace Serveo.Domain.Abstractions.Cache
{
    public sealed class CacheOptions
    {
        public TimeSpan Ttl { get; init; } = TimeSpan.FromMinutes(5);

        // anti cache stampede
        public TimeSpan Jitter { get; init; } = TimeSpan.FromSeconds(10);

        // for session / sliding data
        public TimeSpan? SlidingExpiration { get; init; }

        // Is there a null cache?
        public bool CacheNull { get; init; } = false;
    }
}
