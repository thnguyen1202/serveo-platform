using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Users
{
    public class UpdateUserModel : CommonUserModel, IEntityModel<long>
    {
        public long Id { get; set; }

        [StringLength(32)]
        public string? Password { get; set; }
    }
}
