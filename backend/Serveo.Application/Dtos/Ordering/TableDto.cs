using Serveo.Application.Dtos.Tenanting;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Dtos.Ordering
{
    public sealed class TableDto : EntityDto
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public string PublicToken { get; set; } = default!;
        public TableStatus Status { get; set; }

        public BranchDto? Branch { get; set; } = null;
    }
}
