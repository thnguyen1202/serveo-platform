using Serveo.Domain.Entities.SaaS;

namespace Serveo.Application.Services.Commerce.Staffs.Dto
{
    public class StaffDto
    {
        public long Id { get; set; }
        public Guid Uid { get; set; }
        public int RestaurantId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string DisplayName { get; set; } = default!;
        public string RestaurantName { get; set; } = default!;
        public StaffRoleType Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
