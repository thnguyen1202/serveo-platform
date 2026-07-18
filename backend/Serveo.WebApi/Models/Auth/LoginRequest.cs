using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Auth
{
    public sealed record LoginRequest(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password,

        //string? TenantCode = null,
        string? DeviceId = null,

        string? DeviceName = null
    );

    //public sealed class LoginRequest
    //{
    //    [Required]
    //    [EmailAddress]
    //    public string Email { get; set; } = default!;

    //    [Required]
    //    public string Password { get; set; } = default!;

    //    public string? TenantCode { get; set; }
    //    public string? DeviceId { get; set; }
    //    public string? DeviceName { get; set; }
    //}
}
