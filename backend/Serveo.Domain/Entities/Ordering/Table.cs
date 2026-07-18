using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Domain.Entities.Ordering
{
    public sealed class Table : FullAuditableEntity
    {
        public Guid BranchId { get; set; }

        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }

        public string PublicToken { get; set; } = default!;
        public TableStatus Status { get; set; } = TableStatus.Available;


        public Branch Branch { get; set; } = default!;
    }

    public enum TableStatus : byte
    {
        Available = 1,
        Occupied,
        Reserved,
        Cleaning,
        Disabled
    }
}
