namespace Serveo.WebApi.Models.Catalog.Categories
{
    public class UpdateCategoryModel : CreateCategoryRequest
    {
        public string Id { get; set; } = default!;
    }
}
