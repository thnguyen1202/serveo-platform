namespace Serveo.Application.Services.Commerce.Tables.Dto
{
    public class TableDto
    {
        public long Id { get; set; }
        public int DinerId { get; set; }

        public string DinerName { get; set; } = default!;
        public string Name { get; set; } = default!;

        public int Capacity { get; set; }

        public string QrToken { get; set; } = default!;

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }
    }
}