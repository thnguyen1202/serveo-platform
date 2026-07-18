namespace Serveo.Application.Dtos.Identity
{
    public sealed class RoleDto : EntityDto
    {
        public Guid? TenantId { get; set; }
        public bool IsStatic { get; set; }
        public bool IsDefault { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}