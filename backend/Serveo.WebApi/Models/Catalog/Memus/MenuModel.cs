namespace Serveo.WebApi.Models.Catalog.Memus
{
    public class MenuModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        //public int SortOrder { get; set; }
        //public int RestaurantId { get; set; }
        //public string RestaurantName { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
