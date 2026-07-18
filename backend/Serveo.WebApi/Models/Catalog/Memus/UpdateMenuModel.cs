using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Catalog.Memus
{
    public class UpdateMenuModel : CreateMenuRequest
    {
        [Required]
        public string Id { get; set; } = default!;
    }
}
