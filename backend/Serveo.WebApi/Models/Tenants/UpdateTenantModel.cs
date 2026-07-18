namespace Serveo.WebApi.Models.Tenants
{
    public class UpdateTenantModel : CreateTenantModel, IEntityModel<long>
    {
        public long Id { get; set; }
    }
}
