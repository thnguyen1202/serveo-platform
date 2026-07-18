using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Catalog.Memus
{
    public class CreateMenuRequest
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = default!;

        [StringLength(512)]
        public string? Description { get; set; }

        public Guid BusinessId { get; set; }

    }
}
