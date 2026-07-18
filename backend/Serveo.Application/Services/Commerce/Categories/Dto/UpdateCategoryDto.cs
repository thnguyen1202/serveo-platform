namespace Serveo.Application.Services.Commerce.Categories.Dto
{
    public class UpdateCategoryDto : CreateCategoryDto
    {
        public Guid? Id { get; set; }
        public int SortOrder { get; set; }
    }
}