using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Features.Ordering.OrderSessions.Create
{
    public class CreateOrderSessionResult
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public string PublicToken { get; set; } = default!;
        public TableStatus Status { get; set; }
    }
}