namespace Serveo.Application.Features.Catalog.Products.Create
{
    public class CreateProductResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}