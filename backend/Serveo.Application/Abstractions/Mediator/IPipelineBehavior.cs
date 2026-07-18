namespace Serveo.Application.Abstractions.Mediator
{
    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(
            TRequest request,
            CancellationToken ct,
            RequestHandlerDelegate<TResponse> next);
    }

    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
}
