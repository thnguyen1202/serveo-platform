namespace Serveo.Application.Dtos.Catalog
{
    public class BranchProductDto
    {
        public Guid BranchId { get; set; }
        public Guid ProductId { get; set; }
        public decimal? PriceOverride { get; set; }

        public bool IsActive { get; set; }
    }
}
