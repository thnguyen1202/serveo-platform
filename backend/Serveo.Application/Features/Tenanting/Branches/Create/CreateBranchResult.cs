namespace Serveo.Application.Features.Tenanting.Branches.Create
{
    public class CreateBranchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}