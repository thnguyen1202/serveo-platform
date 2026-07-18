namespace Serveo.WebApi.Models.Restaurants
{
    public class UpdateRestaurantModel : CreateRestaurantModel, IEntityModel<string>
    {
        public string Id { get; set; } = default!;
    }
}
