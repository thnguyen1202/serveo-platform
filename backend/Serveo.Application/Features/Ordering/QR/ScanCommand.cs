using Serveo.Application.Abstractions.Mediator;

namespace Serveo.Application.Features.Ordering.QR
{
    public sealed record ScanCommand(
        string Token
    ) : ICommand<ScanResult>
    {
    }
}
