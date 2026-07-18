using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Phân nhóm khách. ex: VIP, Office, Family, Student
    public class CustomerTag : Entity
    {
        public Guid StoreId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = default!;
    }
}
