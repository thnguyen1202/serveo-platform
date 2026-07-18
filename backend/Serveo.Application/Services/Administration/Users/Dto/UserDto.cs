namespace Serveo.Application.Services.Administration.Users.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? TimeZoneId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
