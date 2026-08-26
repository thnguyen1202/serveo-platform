using Serveo.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Auth
{
    public class RefreshTokenRequest
    {
        //[Required]
        //public string RefreshToken { get; init; } = default!;

        [Required]
        public string DeviceId { get; init; } = default!;

        public ClientType ClientType { get; init; }
    }
}
