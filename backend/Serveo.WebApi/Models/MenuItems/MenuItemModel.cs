namespace Serveo.WebApi.Models.MenuItems
{
    public class MenuItemModel
    {
        public string Id { get; set; } = default!;
        public string? CategoryId { get; set; }
        public string? MenuId { get; set; }
        public string? CategoryName { get; set; }
        public string? MenuName { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
