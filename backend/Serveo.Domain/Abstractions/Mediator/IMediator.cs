namespace Serveo.Domain.Abstractions.Mediator
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct);
    }
}
