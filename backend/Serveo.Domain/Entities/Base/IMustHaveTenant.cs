namespace Serveo.Domain.Entities.Base
{
    public interface IMustHaveTenant
    {
        Guid TenantId { get; set; }
    }
}