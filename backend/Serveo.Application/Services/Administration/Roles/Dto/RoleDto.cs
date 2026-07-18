namespace Serveo.Application.Services.Administration.Roles.Dto
{
    public class RoleDto
    {
        public long Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; } = default!;
        public virtual bool IsStatic { get; set; }
        public virtual bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
    }
}