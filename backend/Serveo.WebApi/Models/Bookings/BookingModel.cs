

namespace Serveo.WebApi.Models.Bookings
{
    public class BookingModel
    {
        public string Id { get; set; } = default!;
        public string TableId { get; set; } = default!;
        public DateTime BookingTime { get; set; }
        //public BookingStatusType Status { get; set; }
    }
}
