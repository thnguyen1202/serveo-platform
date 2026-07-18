namespace Serveo.Application.Services.Commerce.Categories.Dto
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public int DinerId { get; set; }
        public string DinerName { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
    }
}