using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Catalog.Categories
{
    public class CreateCategoryRequest
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = default!;

        public Guid BusinessId { get; set; }
    }
}
