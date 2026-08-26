using Serveo.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Auth
{
    public sealed record LoginRequest(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password,

        bool IsRemember,

        string DeviceId,

        ClientType ClientType
    );
}
