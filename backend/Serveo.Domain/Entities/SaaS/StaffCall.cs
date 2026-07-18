using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Khách bấm gọi phục vụ.
    public class StaffCall : Entity, IHasCreatedAt
    {
        public Guid TableSessionId { get; set; }
        public StaffCallType Type { get; set; }

        [MaxLength(512)]
        public string? Message { get; set; }

        public byte Status { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        //HandledBy
        //HandledAt

        public StaffCall()
        {
            Id = Guid.CreateVersion7();
        }
    }

    public enum StaffCallType : byte
    {
        Service = 1,
        Payment = 2,
        Water = 3,
        BillRequest = 4,
        Assistance = 5,
        Other
    }
}
