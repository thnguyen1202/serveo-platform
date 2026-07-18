using Serveo.WebApi.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Staffs
{
    public class CreateStaffModel : CommonUserModel
    {
        [Required]
        [StringLength(32)]
        public string Password { get; set; } = default!;
        //public StaffRoleType Role { get; set; }

    }
}
