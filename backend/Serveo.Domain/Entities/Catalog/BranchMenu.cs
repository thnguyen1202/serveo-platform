using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public class BranchMenu : IPassivable
    {
        public Guid BranchId { get; set; }
        public Guid MenuId { get; set; }
        public bool IsActive { get; set; }
    }
}
