using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public class BranchCategory : IPassivable
    {
        public Guid BranchId { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
