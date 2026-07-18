using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Users
{
    public class CreateUserModel : CommonUserModel
    {
        [Required]
        [StringLength(32)]
        public string Password { get; set; } = default!;

        public int? RestaurantId { get; set; }
    }
}
