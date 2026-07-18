using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.MenuItems
{
    public class CreateMenuItemModel
    {
        public string CategoryId { get; set; } = default!;
        public string MenuId { get; set; } = default!;

        [Required]
        [StringLength(256)]
        public string Name { get; set; } = default!;

        [StringLength(1024)]
        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
