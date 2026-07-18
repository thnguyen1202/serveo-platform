using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Giới thiệu bạn bè.
    public class ReferralProgram : Entity
    {
        public Guid StoreId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; } = default!;
        public int RewardPoints { get; set; }
    }
}
