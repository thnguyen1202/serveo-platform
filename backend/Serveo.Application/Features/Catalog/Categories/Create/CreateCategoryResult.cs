namespace Serveo.Application.Features.Catalog.Categories.Create
{
    public class CreateCategoryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}