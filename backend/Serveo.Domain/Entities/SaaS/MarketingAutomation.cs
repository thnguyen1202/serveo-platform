using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Workflow tự động. ex: Sinh nhật khách => gửi voucher 20%
    public class MarketingAutomation : Entity, IPassivable
    {
        public Guid StoreId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = default!;

        [MaxLength(64)]
        public string TriggerType { get; set; } = default!;
        public string? ActionJson { get; set; }
        public bool IsActive { get; set; }
    }
}
