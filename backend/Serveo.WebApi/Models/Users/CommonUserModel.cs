using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Users
{
    public class CommonUserModel
    {
        [Required]
        [StringLength(128)]
        public string UserName { get; set; } = default!;

        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; } = default!;

        [StringLength(32)]
        public string? PhoneNumber { get; set; }

        //public int? RoleId { get; set; }

        //[StringLength(64)]
        //public string? TimeZoneId { get; set; } = "Singapore Standard Time";

        //[StringLength(256)]
        //public string? Avatar { get; set; }

        //public int? TenantId { get; set; }
    }
}
