using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Chiến dịch marketing.
    public class Campaign : Entity
    {
        public int StoreId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; } = default!;
        public CampaignType Type { get; set; }
        public int Status { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

    }

    public enum CampaignType : byte
    {
        SMS,
        Email,
        Push,
        Zalo
    }
}
