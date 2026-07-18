using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class AuditLog : CreationAuditableEntity, IMayHaveTenant, IMayHaveUser
    {
        public Guid? TenantId { get; set; }
        public Guid? UserId { get; set; }

        public string TableName { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string KeyValues { get; set; } = default!;

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? ChangedColumns { get; set; }

        public string? Metadata { get; set; } // IP, UserAgent, Device, AppVersion, request_id, trace_id
    }

    /*
     * Action nên chuẩn hoá
     CREATE
UPDATE
DELETE
SOFT_DELETE
LOGIN
LOGOUT
FAILED_LOGIN
APPROVE
CANCEL
     */

    /*
     Index chuẩn SaaS
    INDEX ix_audit_tenant_entity
    (tenant_id, entity_name, entity_id)

INDEX ix_audit_tenant_time
    (tenant_id, created_at)

INDEX ix_audit_action
    (action)
     */
}
