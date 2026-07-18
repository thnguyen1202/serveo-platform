using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Tenanting.Outlets
{
    public sealed record CreateOutletRequest(
        [Required]
        [StringLength(256)]
        string Name,

        [StringLength(256)]
        string? Address
    );
}
