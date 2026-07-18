using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Phân khúc tự động. ex
    //{
    //  "visitCount": ">5",
    //  "totalSpend": ">3000000"
    //}
    public class CustomerSegment : Entity
    {
        public Guid StoreId { get; set; }

        [MaxLength(128)]
        public string? Name { get; set; }
        public string? RuleJson { get; set; }

    }
}
