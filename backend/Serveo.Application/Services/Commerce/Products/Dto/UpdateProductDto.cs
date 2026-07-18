namespace Serveo.Application.Services.Commerce.Products.Dto
{
    public class UpdateProductDto : CreateProductDto
    {
        public Guid? Id { get; set; }
    }
}