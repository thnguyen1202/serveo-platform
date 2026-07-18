using Serveo.WebApi.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Staffs
{
    public class UpdateStaffModel : CommonUserModel, IEntityModel<long>
    {
        public long Id { get; set; }

        [StringLength(32)]
        public string? Password { get; set; }
        //public StaffRoleType? Role { get; set; }
    }
}
