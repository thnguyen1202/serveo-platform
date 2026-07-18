namespace Serveo.Application.Dtos.Tenanting
{
    public sealed class BranchDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public bool IsActive { get; set; }

        public BusinessDto? Business { get; set; } = null;
    }
}
