using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [StringLength(128)]
        public string Username { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string Password { get; set; } = default!;

    }
}
