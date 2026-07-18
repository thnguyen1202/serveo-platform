namespace Serveo.Application.Behaviors
{
    //public class QueryCacheBehavior<TRequest, TResponse>
    //: IPipelineBehavior<TRequest, TResponse>
    //where TRequest : ICacheableQuery
    //{
    //    private readonly ICacheService _cache;

    //    public QueryCacheBehavior(ICacheService cache)
    //    {
    //        _cache = cache;
    //    }

    //    public async Task<TResponse> Handle(
    //        TRequest request,
    //        RequestHandlerDelegate<TResponse> next,
    //        CancellationToken ct)
    //    {
    //        var cacheKey = request.CacheKey;

    //        return await _cache.GetOrCreateAsync(
    //            cacheKey,
    //            _ => next(),
    //            new CacheOptions
    //            {
    //                Ttl = request.Expiration ?? TimeSpan.FromMinutes(5),
    //                CacheNull = true
    //            },
    //            ct);
    //    }
    //}
}
