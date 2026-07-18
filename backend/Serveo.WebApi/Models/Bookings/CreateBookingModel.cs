using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Bookings
{
    public class CreateBookingModel
    {
        //public int RestaurantId { get; set; }
        public long TableId { get; set; }

        [StringLength(256)]
        public string? CustomerName { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? CustomerEmail { get; set; }

        [StringLength(32)]
        public string? CustomerPhone { get; set; }
    }
}
