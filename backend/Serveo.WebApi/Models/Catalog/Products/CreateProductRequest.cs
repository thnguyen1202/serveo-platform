using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Catalog.Products
{
    public class CreateProductRequest
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = default!;

        [StringLength(512)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public Guid CategoryId { get; set; }
        public Guid BusinessId { get; set; }
    }
}
