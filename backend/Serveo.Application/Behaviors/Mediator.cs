using Microsoft.Extensions.DependencyInjection;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Behaviors
{
    public sealed class Mediator(IServiceProvider sp) : IMediator
    {
        private readonly IServiceProvider _sp = sp;

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
        {
            var requestType = request.GetType();

            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(requestType, typeof(TResponse));

            dynamic handler = _sp.GetRequiredService(handlerType);

            return await handler.HandleAsync((dynamic)request, ct);
        }
    }
}
