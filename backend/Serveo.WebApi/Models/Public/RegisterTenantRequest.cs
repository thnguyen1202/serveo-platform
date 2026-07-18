using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Public
{
    public class RegisterTenantRequest
    {
        [Required]
        [StringLength(128, MinimumLength = 2)]
        public string TenantName { get; set; } = default!;

        [Required]
        [StringLength(128)]
        public string FullName { get; set; } = default!;

        [Required]
        [StringLength(256)]
        public string Email { get; set; } = default!;

        [StringLength(32)]
        public string Password { get; set; } = default!;

        [StringLength(32)]
        public string? Phone { get; set; }



        [StringLength(256)]
        public string? Address { get; set; }

        [StringLength(128)]
        public string? TimeZone { get; set; }

    }
}
