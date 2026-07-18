namespace Serveo.Application.Services.Commerce.Categories.Dto
{
    public class CreateCategoryDto
    {
        public Guid? StoreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
