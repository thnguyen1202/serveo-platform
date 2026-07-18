namespace Serveo.Application.Dtos.Catalog
{
    public class BranchCategoryDto
    {
        public Guid BranchId { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
