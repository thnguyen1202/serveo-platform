using Serveo.Domain.Entities.Ordering;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.WebApi.Models.Public
{
    public sealed class InitializationResponse
    {
        public InitBusinessResponse Business { get; set; } = default!;
        public InitBranchResponse Branch { get; set; } = default!;
        public InitTableResponse Table { get; set; } = default!;
        public object? Theme { get; set; }
        public InitFeaturesResponse Features { get; set; } = new InitFeaturesResponse();
        public string OrderMode { get; set; } = "DineIn";
    }

    public class InitFeaturesResponse
    {
        public bool TakeAway { get; set; } = false;
        public bool CallStaff { get; set; } = true;
        public bool SplitBill { get; set; } = true;
    }

    public class InitBusinessResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public BusinessType? Type { get; set; }
        public string? Currency { get; init; }
        public string? Language { get; init; }
    }

    public sealed class InitBranchResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
    }

    public class InitTableResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
    }
}
