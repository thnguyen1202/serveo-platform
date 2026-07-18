using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS
{
    public class CampaignDelivery : Entity
    {
        public Guid CampaignId { get; set; }
        public Guid CustomerId { get; set; }

        public DateTimeOffset DeliveredAt { get; set; }

        public DateTimeOffset OpenedAt { get; set; }

        public DateTimeOffset ClickedAt { get; set; }
        public byte Status { get; set; }
    }
}
