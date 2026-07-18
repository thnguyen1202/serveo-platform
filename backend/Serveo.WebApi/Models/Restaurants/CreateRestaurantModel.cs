using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Restaurants
{
    public class CreateRestaurantModel
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = default!;

        [Required]
        [StringLength(256)]
        public string Slug { get; set; } = default!;
    }
}
