using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS.Analytics
{
    // Dữ liệu tổng hợp.
    public class AnalyticsSnapshot : Entity
    {
        public Guid StoreId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Revenue { get; set; }
        public int OrderCount { get; set; }
        public int CustomerCount { get; set; }
        public int ReturningCustomerCount { get; set; }
        public decimal AverageOrderValue { get; set; }

    }
}
