namespace Serveo.WebApi.Models.Roles
{
    public class UpdateRoleModel : CreateRoleModel, IEntityModel<long>
    {
        public long Id { get; set; }
    }
}
