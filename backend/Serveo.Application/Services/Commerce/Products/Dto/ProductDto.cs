namespace Serveo.Application.Services.Commerce.Products.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long CategoryName { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
    }
}