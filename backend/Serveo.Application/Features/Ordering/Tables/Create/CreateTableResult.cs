using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Features.Ordering.Tables.Create
{
    public class CreateTableResult
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public string PublicToken { get; set; } = default!;
        public TableStatus Status { get; set; }
    }
}