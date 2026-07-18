namespace Serveo.Domain.Abstractions.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken ct = default);

        Task<T> GetOrCreateAsync<T>(string? key, Func<CancellationToken, Task<T>> factory, CacheOptions options, CancellationToken ct = default);

        Task SetAsync<T>(string key, T value, CacheOptions options, CancellationToken ct = default);

        Task RemoveAsync(string? key, CancellationToken ct = default);
    }
}
