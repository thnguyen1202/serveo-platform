namespace Serveo.Application.Services.Commerce.Tables.Dto
{
    public class CreateTableDto
    {
        public Guid? StoreId { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; } = 2;
    }
}