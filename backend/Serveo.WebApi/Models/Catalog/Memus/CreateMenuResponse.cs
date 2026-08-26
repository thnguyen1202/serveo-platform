namespace Serveo.WebApi.Models.Catalog.Memus
{
    public class CreateMenuResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
