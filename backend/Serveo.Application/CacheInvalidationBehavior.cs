namespace Serveo.Application
{
    //public class CacheInvalidationBehavior<TRequest, TResponse>
    //: IPipelineBehavior<TRequest, TResponse>
    //where TRequest : ICacheInvalidationCommand
    //{
    //    private readonly ICacheService _cache;

    //    public CacheInvalidationBehavior(ICacheService cache)
    //    {
    //        _cache = cache;
    //    }

    //    public async Task<TResponse> Handle(
    //        TRequest request,
    //        RequestHandlerDelegate<TResponse> next,
    //        CancellationToken ct)
    //    {
    //        var result = await next();

    //        await _cache.RemoveAsync(request.CacheKeyRemoveItem, ct);
    //        await _cache.RemoveAsync(request.CacheKeyRemoveList, ct);

    //        return result;
    //    }
    //}
}
