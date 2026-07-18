namespace Serveo.Application.Behaviors
{
    //public sealed class CacheBehavior<TRequest, TResponse>
    //: IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly ICacheService _cache;

    //    public async Task<TResponse> Handle(
    //        TRequest request,
    //        CancellationToken ct,
    //        RequestHandlerDelegate<TResponse> next)
    //    {
    //        if (request is not ICacheable cacheable)
    //            return await next();

    //        var cached = await _cache.GetAsync<TResponse>(
    //            cacheable.CacheKey);

    //        if (cached is not null)
    //            return cached;

    //        var response = await next();

    //        await _cache.SetAsync(
    //            cacheable.CacheKey,
    //            response,
    //            cacheable.Expiration);

    //        return response;
    //    }
    //}

    //public sealed class CacheBehavior<TRequest, TResponse>(
    //ICacheService cache)
    //: IPipelineBehavior<TRequest, TResponse>
    //where TRequest : IRequest<TResponse>
    //{
    //    private readonly ICacheService _cache = cache;

    //    public async Task<TResponse> Handle(
    //        TRequest request,
    //        RequestHandlerDelegate<TResponse> next,
    //        CancellationToken ct)
    //    {
    //        if (request is not ICacheable cacheable)
    //            return await next();

    //        var cached =
    //            await _cache.GetAsync<TResponse>(
    //                cacheable.CacheKey,
    //                ct);

    //        if (cached is not null)
    //            return cached;

    //        var response = await next();

    //        await _cache.SetAsync(
    //            cacheable.CacheKey,
    //            response,
    //            cacheable.Expiration,
    //            ct);

    //        return response;
    //    }
    //}
}
