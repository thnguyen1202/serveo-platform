using Serveo.WebApi.Models.Catalog.Categories;
using System.Text.Json.Serialization;

namespace Serveo.WebApi.Models.Catalog.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CategoryResponse? Category { get; set; } = null;
    }
}