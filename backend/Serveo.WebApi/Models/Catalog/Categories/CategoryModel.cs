namespace Serveo.WebApi.Models.Catalog.Categories
{
    public class CategoryModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        //public int SortOrder { get; set; }
        //public int RestaurantId { get; set; }
        //public string RestaurantName { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
