using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Roles
{
    public class CreateRoleModel
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = default!;

        public int RestaurantId { get; set; }

    }
}
