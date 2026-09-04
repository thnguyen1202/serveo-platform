using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Catalog.Categories
{
    public class CreateCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
