namespace Serveo.Application.Services.Commerce.Menus.Dto
{
    public class CreateMenuDto
    {
        public Guid? StoreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}