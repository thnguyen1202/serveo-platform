using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Auth
{
    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; } = default!;
    }
}
