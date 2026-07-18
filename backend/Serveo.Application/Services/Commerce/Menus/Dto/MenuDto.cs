namespace Serveo.Application.Services.Commerce.Menus.Dto
{
    public class MenuDto
    {
        public long Id { get; set; }
        public int DinerId { get; set; }
        public string DinerName { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }

        public List<MenuItemDto> MenuItems { get; set; } = [];
    }

    public class MenuItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}