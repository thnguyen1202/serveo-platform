using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse>(IUnitOfWork uow)
    : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _uow = uow;

        public Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
        {
            throw new NotImplementedException();
        }

        //public async Task<TResponse> Handle(
        //    TRequest request,
        //    CancellationToken ct,
        //    RequestHandlerDelegate<TResponse> next)
        //{
        //    await _uow.BeginTransactionAsync(ct);

        //    try
        //    {
        //        var response = await next();

        //        await _uow.CommitAsync(ct);

        //        return response;
        //    }
        //    catch
        //    {
        //        await _uow.RollbackAsync(ct);
        //        throw;
        //    }
        //}
    }
}
