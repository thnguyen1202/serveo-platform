namespace Serveo.Application.Dtos.Catalog
{
    public class BranchMenuDto
    {
        public Guid BranchId { get; set; }
        public Guid MenuId { get; set; }
        public bool IsActive { get; set; }
    }
}
