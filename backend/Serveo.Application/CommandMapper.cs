using Riok.Mapperly.Abstractions;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Application
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
    public partial class CommandMapper
    {
        public partial MenuDto ToDto(Menu result);
        public partial List<MenuDto> ToDtoList(IEnumerable<Menu> items);
        public partial IQueryable<MenuDto> ProjectToDto(IQueryable<Menu> query);

        public partial CreateMenuResult ToResult(Menu result);
    }
}
