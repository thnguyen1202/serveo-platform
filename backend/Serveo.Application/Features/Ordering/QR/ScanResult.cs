using Serveo.Application.Dtos.Ordering;

namespace Serveo.Application.Features.Ordering.QR
{
    public class ScanResult
    {
        public TableDto Table { get; init; } = default!;
        public object? Theme { get; init; }
    }


}