namespace Serveo.Application.Abstractions.Mediator
{
    internal interface ICommandHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken ct);
    }
}
