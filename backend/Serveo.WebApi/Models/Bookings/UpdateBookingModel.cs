namespace Serveo.WebApi.Models.Bookings
{
    public class UpdateBookingModel : CreateBookingModel, IEntityModel<long>
    {
        public long Id { get; set; }
    }
}
