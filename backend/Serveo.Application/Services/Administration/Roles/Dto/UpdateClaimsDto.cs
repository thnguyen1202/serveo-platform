namespace Serveo.Application.Services.Administration.Roles.Dto
{
    public class UpdateClaimsDto
    {
        public Guid? Id { get; set; }
        public List<string> Claims { get; set; } = default!;
    }
}
