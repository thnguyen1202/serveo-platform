namespace Serveo.Application.Services.Administration.Users.Dto
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string DisplayName { get; set; } = default!;
        public string? Avatar { get; set; }
        public int? RestaurantId { get; set; }
        public string? Password { get; internal set; }
    }
}
