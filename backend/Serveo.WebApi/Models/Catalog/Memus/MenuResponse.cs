using Serveo.WebApi.Models.Catalog.Products;
using System.Text.Json.Serialization;

namespace Serveo.WebApi.Models.Catalog.Memus
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<ProductResponse>? Products { get; set; } = null;
    }
}
