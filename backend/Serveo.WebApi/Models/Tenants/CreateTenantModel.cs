using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Tenants
{
    public class CreateTenantModel
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = default!;

        public int RestaurantId { get; set; }

    }
}
