using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS
{
    // Danh sách khách nhận.
    public class CampaignAudience : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid CustomerId { get; set; }
        public byte Status { get; set; }

    }
}
