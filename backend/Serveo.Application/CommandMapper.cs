using Riok.Mapperly.Abstractions;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Dtos.Ordering;
using Serveo.Application.Features.Catalog.Categories.Create;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Application.Features.Catalog.Products.Create;
using Serveo.Application.Features.Ordering.Tables.Create;
using Serveo.Domain.Entities.Catalog;
using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
    public partial class CommandMapper
    {
        #region ToDto
        public partial MenuDto ToDto(Menu result);
        public partial MenuTranslationDto ToDto(MenuTranslation result);

        public partial CategoryDto ToDto(Category result);
        public partial CategoryTranslationDto ToDto(CategoryTranslation result);

        public partial ProductDto ToDto(Product result);
        public partial ProductTranslationDto ToDto(ProductTranslation result);

        public partial TableDto ToDto(Table result);

        public partial List<MenuDto> ToDtoList(IEnumerable<Menu> items);
        public partial List<CategoryDto> ToDtoList(IEnumerable<Category> items);

        public partial IQueryable<MenuDto> ProjectToDto(IQueryable<Menu> query);
        #endregion

        #region ToResult
        public partial CreateMenuResult ToResult(Menu result);
        public partial CreateCategoryResult ToResult(Category result);
        public partial CreateProductResult ToResult(Product result);
        public partial CreateTableResult ToResult(Table result);
        #endregion
    }
}
