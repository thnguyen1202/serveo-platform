using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public class BranchProduct : IPassivable
    {
        public Guid BranchId { get; set; }
        public Guid ProductId { get; set; }
        public decimal? PriceOverride { get; set; }

        public bool IsActive { get; set; }
    }
}
