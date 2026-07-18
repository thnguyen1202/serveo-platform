namespace Serveo.Application.Services.Commerce.Staffs.Dto
{
    public class CreateStaffDto
    {
        public string? UserName { get; set; }
        public string Email { get; set; } = default!;
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DisplayName { get; set; }
        public string? Avatar { get; set; }
        public string? TimeZoneId { get; set; }
        public Guid StoreId { get; set; }
    }
}
